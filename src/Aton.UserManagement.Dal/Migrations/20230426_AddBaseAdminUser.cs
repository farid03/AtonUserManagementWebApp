using System.Security.Cryptography;
using Aton.UserManagement.Dal.Models;
using Aton.UserManagement.Dal.Repositories;
using Aton.UserManagement.Dal.Repositories.Interfaces;
using FluentMigrator;

namespace Aton.UserManagement.Dal.Migrations;

[Migration(20230426, TransactionBehavior.None)]
public class AddBaseAdminUser : Migration
{
    private readonly IUsersRepository _usersRepository;

    public AddBaseAdminUser(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public override void Up()
    {
        const string pattern = @"
INSERT INTO aton_user (login, password, name, gender, admin, created_on, created_by) 
VALUES ('admin', '{0}', 'admin', 2, true, CURRENT_TIMESTAMP, '')
";
        var sql = string.Format(pattern, ComputeHash("password"));
                
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DELETE FROM aton_user WHERE login = 'admin'
";
        
        Execute.Sql(sql);
    }

    private string ComputeHash(string inputString)
    {
        using var md5 = MD5.Create();

        var data = System.Text.Encoding.ASCII.GetBytes(inputString);
        data = md5.ComputeHash(data);
        var hash = Convert.ToBase64String(data);

        return hash;
    }
}