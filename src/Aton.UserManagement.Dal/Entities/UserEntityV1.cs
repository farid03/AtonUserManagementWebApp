namespace Aton.UserManagement.Dal.Entities;

public record UserEntityV1
{
    public int Guid { get; init; }

    public string Login { get; init; }

    public string Password { get; init; }

    public string Name { get; init; }

    public int Gender { get; init; }

    public DateTime? Birthday { get; init; }

    public bool Admin { get; init; }

    public DateTime CreatedOn { get; init; }
    
    public string CreatedBy { get; init; }
    
    public DateTime ModifiedOn { get; init; }
    
    public string ModifiedBy { get; init; }
    
    public DateTime RevokedOn { get; init; }
    
    public string RevokedBy { get; init; }
}