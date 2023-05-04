using Aton.UserManagement.Dal.Entities;
using Aton.UserManagement.Dal.Models;
using Aton.UserManagement.Dal.Repositories.Interfaces;
using Aton.UserManagement.Dal.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace Aton.UserManagement.Dal.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    // TODO
    public UsersRepository(
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<int> Add(string ownerLogin, UserModel user, CancellationToken token)
    {
        const string sqlQuery = @"
INSERT INTO aton_user (login, password, name, gender, birthday, admin, created_on, created_by)
VALUES (@Login, @Password, @Name, @Gender, @Birthday, @Admin, CURRENT_TIMESTAMP, @CreatedBy)
returning guid;
";
        var sqlQueryParams = new
        {
            Login = user.Login,
            Password = user.Password,
            Name = user.Name,
            Gender = user.Gender,
            Birthday = user.Birthday,
            Admin = user.Admin,
            CreatedBy = ownerLogin,
        };

        await using var connection = await GetAndOpenConnection();
        var ids = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));

        return ids.First();
    }

    public Task<int> Update(string modifierLogin, UserModel user, CancellationToken token)
    {
        throw new NotImplementedException();
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
            Login = login,
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
            LimitAge = age,
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
            Login = login,
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
            RevokerLogin = revokerLogin,
        };

        await using var connection = await GetAndOpenConnection();

        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
    }

    public Task<int> Restore(string login, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}