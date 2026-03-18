using PracticeBuildCSharp.Repository.Abstraction;

namespace PracticeBuildCSharp.Repository.Entity;

public class Storage : BaseEntity<Guid>, IAuditableEntity
{
    public decimal Price { get; set; }
    public required string Type { get; set; }

    public ICollection<ProductStorage> ProductStorages = new List<ProductStorage>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}