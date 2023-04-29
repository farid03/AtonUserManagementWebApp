using System.Text.Json;
using Aton.UserManagement.Api.Extensions;

namespace Aton.UserManagement.Api.NamingPolicies;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}