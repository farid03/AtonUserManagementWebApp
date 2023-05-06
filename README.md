# User Managment Application

Web API сервис реализующий API методы CRUD над
сущностью Users.

- Доступ к API осуществляется через интерфейс Swagger.

- Все действия сохраняются в базе данных PostgreSQL.

- Предварительно создается пользователь:
    ```yaml
    login: admin 
    password: password
    ```
  от имени которого можно производить начальные действия.

## Сборка и запуск

Базы данных:

```shell
docker compose up -d
```

Web API:

```shell
dotnet run --project src/Aton.UserManagement.Api/Aton.UserManagement.Api.csproj
```

## API

### Create User

Создание пользователя по указанным параметрам.
> Доступно только администраторам.

```
POST /v1/create
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_to_create": {
    "login": "string",
    "password": "string",
    "name": "string",
    "gender": 0,
    "birthday": "2023-04-30T17:42:52.598Z",
    "admin": true
  }
}
=> 200 {
  "created_user_id": 0
}
=> 400 UserNotFoundException 
        or LoginAlreadyExistsException 
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```

### Delete User
#### Soft
Мягкое удаление пользователя по login.
> Доступно только администраторам.
```
DELETE /v1/delete/soft
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_login": "string"
}
=> 200
=> 400 UserNotFoundException 
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```

#### Hard
Полное удаление пользователя по login.
> Доступно только администраторам.
```
DELETE /v1/delete/hard
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_login": "string"
}
=> 200
=> 400 UserNotFoundException 
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```

### Read
#### All active users
Запрос списка всех активных пользователей отсортированного по дате создания.
> - Доступно только администраторам.
```
POST /v1/read/active
<= {
  "principal": {
    "login": "string",
    "password": "string"
  }
}
=> 200 {
  "users": [
    {
      "name": "string",
      "gender": 0,
      "birthday": "2023-05-06T21:10:44.417Z",
      "is_admin": true
    }
  ]
}
=> 400 InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```
#### By login
Запрос информации о пользователе по логину.
> - Доступно только администраторам.
```
POST /v1/read/login
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_login": "string"
}
=> 200 {
  "name": "string",
  "gender": 0,
  "birthday": "2023-05-06T21:13:20.084Z",
  "is_active": true
}
=> 400 UserNotFoundException
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```
#### Myself
Запрос информации о пользователе по логину и паролю.
> - Доступно только самому пользователю, если он активен.
```
POST /v1/read/myself
<= {
  "principal": {
    "login": "string",
    "password": "string"
  }
}
=> 200 {
  "name": "string",
  "gender": 0,
  "birthday": "2023-05-06T21:14:19.720Z",
  "is_active": true
}
=> 400 UserNotFoundException
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```
#### Older than
Запрос списка всех пользователей старше определенного возраста.
> - Доступно только администраторам.
```
POST /v1/read/older
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "age": 0
}
=> 200 {
  "users": [
    {
      "name": "string",
      "gender": 0,
      "birthday": "2023-05-06T21:16:58.198Z",
      "is_admin": true
    }
  ]
}
=> 400 InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```

### Update User
#### Info
Обновление имени, пола и даты рождения пользователя.
> - Доступно администраторам.
> - Доступно самому пользователю, если он активен.
```
PATCH /v1/update/info
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_login": "string",
  "name": "string",
  "gender": 0,
  "birthday": "2023-05-06T21:19:39.819Z"
}
=> 200 
=> 400 InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```
#### Login
Обновление логина пользователя.
> - Доступно администраторам.
> - Доступно самому пользователю, если он активен.
> - Логин должен оставаться уникальным.
```
PATCH /v1/update/login
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "old_login": "string",
  "new_login": "string"
}
=> 200 
=> 400 LoginAlreadyExistsException
        or UserNotFoundException
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```
#### Password
Обновление пароля пользователя.
> - Доступно администраторам.
> - Доступно самому пользователю, если он активен.
```
PATCH /v1/update/password
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_login": "string",
  "new_password": "string"
}
=> 200 
=> 400 UserNotFoundException
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```
#### Restore
Восстановление пользователя удаленного при помощи soft-delete.
> - Доступно только администраторам.
```
PATCH /v1/update/restore
<= {
  "principal": {
    "login": "string",
    "password": "string"
  },
  "user_login": "string"
}
=> 200 
=> 400 UserNotFoundException
        or InvalidPrincipalCreditsException
        or FluentValidationException
=> 403 ForbiddenException
```