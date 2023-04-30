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

Web API: [status: in development]
```shell
dotnet run --project src/Aton.UserManagement.Api/Aton.UserManagement.Api.csproj
```

gRPC API: [status: in development]
```shell
dotnet run --project src/Aton.UserManagement.Api/Aton.UserManagement.gRPC.Api.csproj
```

## API

### Create
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