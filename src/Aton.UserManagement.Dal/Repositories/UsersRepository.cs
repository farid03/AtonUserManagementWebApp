using Aton.UserManagement.Dal.Entities;
using Aton.UserManagement.Dal.Models;
using Aton.UserManagement.Dal.Repositories.Interfaces;
using Aton.UserManagement.Dal.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace Aton.UserManagement.Dal.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<int> Add(string ownerLogin, UserModel user, CancellationToken token)
    {
        const string sqlQuery = @"
insert into aton_user (login, password, name, gender, birthday, admin, created_on, created_by)
values (@Login, @Password, @Name, @Gender, @Birthday, @Admin, CURRENT_TIMESTAMP, @CreatedBy)
returning guid;
";
        var sqlQueryParams = new
        {
            user.Login,
            user.Password,
            user.Name,
            user.Gender,
            user.Birthday,
            user.Admin,
            CreatedBy = ownerLogin
        };

        await using var connection = await GetAndOpenConnection();
        var ids = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));

        return ids.First();
    }

    public async Task UpdateUserInfo(
        string modifierLogin,
        string userLogin,
        string name,
        int gender,
        DateTime? birthday,
        CancellationToken token)
    {
        const string sqlQuery = @"
update 
    aton_user set name = @Name,
                  gender = @Gender,
                  birthday = @Birthday,
                  modified_on = CURRENT_TIMESTAMP,
                  modified_by = @ModifierLogin
where login = @Login
";
        var sqlQueryParams = new
        {
            Name = name,
            Gender = gender,
            Birthday = birthday,
            ModifierLogin = modifierLogin,
            Login = userLogin
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }

    public async Task UpdateUserPassword(
        string modifierLogin,
        string userLogin,
        string password,
        CancellationToken token)
    {
        const string sqlQuery = @"
update 
    aton_user set password = @Password,
                  modified_on = CURRENT_TIMESTAMP,
                  modified_by = @ModifierLogin
where login = @Login
";
        var sqlQueryParams = new
        {
            Password = password,
            ModifierLogin = modifierLogin,
            Login = userLogin
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }

    public async Task UpdateUserLogin(
        string modifierLogin,
        string oldLogin,
        string newLogin,
        CancellationToken token)
    {
        const string sqlQuery = @"
update 
    aton_user set login = @NewLogin,
                  modified_on = CURRENT_TIMESTAMP,
                  modified_by = @ModifierLogin
where login = @Login
";
        var sqlQueryParams = new
        {
            NewLogin = newLogin,
            ModifierLogin = modifierLogin,
            Login = oldLogin
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }

    public async Task<UserEntityV1[]> GetAllActiveUsers(CancellationToken token)
    {
        const string sqlQuery = @"
select
    login,
    password,
    name,
    gender,
    birthday,
    admin,
    revoked_on
from aton_user 
where revoked_on is null
order by created_on
";
        await using var connection = await GetAndOpenConnection();
        var user = await connection.QueryAsync<UserEntityV1>(
            new CommandDefinition(
                sqlQuery,
                cancellationToken: token));

        return user.ToArray();
    }

    public async Task<UserEntityV1?> Get(string login, CancellationToken token)
    {
        const string sqlQuery = @"
select
    login,
    password,
    name,
    gender,
    birthday,
    admin,
    revoked_on
from aton_user 
where login = @Login
";
        var sqlQueryParams = new
        {
            Login = login
        };

        await using var connection = await GetAndOpenConnection();
        var user = await connection.QueryAsync<UserEntityV1?>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));

        return user.FirstOrDefault((UserEntityV1?)null);
    }

    public async Task<UserEntityV1[]> GetOlderThan(int age, CancellationToken token)
    {
        const string sqlQuery = @"
select
    login,
    password,
    name,
    gender,
    birthday,
    admin,
    revoked_on
from aton_user
where extract(year from age(CURRENT_TIMESTAMP, birthday)) >= @LimitAge
order by created_on
";
        var sqlQueryParams = new
        {
            LimitAge = age
        };

        await using var connection = await GetAndOpenConnection();
        var user = await connection.QueryAsync<UserEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));

        return user.ToArray();
    }

    public async Task Delete(string login, CancellationToken token)
    {
        const string sqlQuery = @"
delete from aton_user
where login = @Login
";
        var sqlQueryParams = new
        {
            Login = login
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }

    public async Task Revoke(string revokerLogin, string login, CancellationToken token)
    {
        const string sqlQuery = @"
update 
    aton_user set revoked_on = CURRENT_TIMESTAMP, 
                  revoked_by = @RevokerLogin
where login = @Login
";
        var sqlQueryParams = new
        {
            Login = login,
            RevokerLogin = revokerLogin
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }

    public async Task Restore(string modifierLogin, string login, CancellationToken token)
    {
        const string sqlQuery = @"
update 
    aton_user set modified_on = CURRENT_TIMESTAMP,
                  modified_by = @ModifierLogin,
                  revoked_on = null, 
                  revoked_by = null
where login = @Login
";
        var sqlQueryParams = new
        {
            Login = login,
            ModifierLogin = modifierLogin
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }
}