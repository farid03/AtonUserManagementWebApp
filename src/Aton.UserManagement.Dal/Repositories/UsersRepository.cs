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

//     public async Task<long[]> Add(
//         GoodEntityV1[] goods,
//         CancellationToken token)
//     {
//         const string sqlQuery = @"
// insert into goods (user_id, width, height, length, weight) 
// select user_id, width, height, length, weight
//   from UNNEST(@Goods)
// returning id;
// ";
//
//         var sqlQueryParams = new
//         {
//             Goods = goods
//         };
//
//         await using var connection = await GetAndOpenConnection();
//         var ids = await connection.QueryAsync<long>(
//             new CommandDefinition(
//                 sqlQuery,
//                 sqlQueryParams,
//                 cancellationToken: token));
//
//         return ids
//             .ToArray();
//     }
//
//     public async Task<GoodEntityV1[]> Query(
//         long userId,
//         CancellationToken token)
//     {
//         const string sqlQuery = @"
// select id
//      , user_id
//      , width
//      , height
//      , length
//      , weight
//   from goods
//  where user_id = @UserId;
// ";
//
//         var sqlQueryParams = new
//         {
//             UserId = userId
//         };
//
//         await using var connection = await GetAndOpenConnection();
//         var goods = await connection.QueryAsync<GoodEntityV1>(
//             new CommandDefinition(
//                 sqlQuery,
//                 sqlQueryParams,
//                 cancellationToken: token));
//
//         return goods
//             .ToArray();
//     }
//
//     public async Task Delete(long[] goodsIds, CancellationToken cancellationToken)
//     {
//         const string sqlQuery = @"
//  delete from goods where id = any(@GoodsIds);
// ";
//         await using var connection = await GetAndOpenConnection();
//
//         var sqlQueryParams = new
//         {
//             GoodsIds = goodsIds
//         };
//
//         await connection.QueryAsync(
//             new CommandDefinition(
//                 sqlQuery,
//                 sqlQueryParams,
//                 cancellationToken: cancellationToken));
//     }

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

    public Task<UserEntityV1[]> GetAllUsers(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntityV1[]> Get(string login, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntityV1[]> GetOlderThan(int age, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task Delete(string login, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Revoke(string login, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> Restore(string login, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}