using PracticeBuildCSharp.Repository.Abstraction;

namespace PracticeBuildCSharp.Repository.Entity;

public class Order : BaseEntity<Guid>, IAuditableEntity
{

    public decimal TotalAmount  { get; set; }
    
    public string Status { get; set; } = "Pending";
    
    public string Address { get; set; }  
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<OrderDetail> OrderDetails { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}