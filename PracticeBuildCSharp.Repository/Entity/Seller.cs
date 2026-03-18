using PracticeBuildCSharp.Repository.Abstraction;

namespace PracticeBuildCSharp.Repository.Entity;

public class Seller : BaseEntity<Guid>, IAuditableEntity
{
    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    public string TaxCode { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Product> Products = new List<Product>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}