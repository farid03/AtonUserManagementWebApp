namespace Aton.UserManagement.Bll.Models;

public record UserModel(
    string Login,
    string Password,
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Admin
);