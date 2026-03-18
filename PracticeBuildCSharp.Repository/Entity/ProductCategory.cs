using PracticeBuildCSharp.Repository.Abstraction;

namespace PracticeBuildCSharp.Repository.Entity;

public class ProductCategory : BaseEntity<Guid>, IAuditableEntity

{
     
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}