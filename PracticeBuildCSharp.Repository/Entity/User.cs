using PracticeBuildCSharp.Repository.Abstraction;

namespace PracticeBuildCSharp.Repository.Entity;

public class User: BaseEntity<Guid>, IAuditableEntity
{
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
    public int? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Role { get; set; } = "User";
    public bool IsVerify { get; set; } = false;
    public int VerifyCode { get; set; }
    
    public ICollection<Order> Orders = new List<Order>();
    
    public Seller? Seller { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}