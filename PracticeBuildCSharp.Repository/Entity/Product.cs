using PracticeBuildCSharp.Repository.Abstraction;

namespace PracticeBuildCSharp.Repository.Entity;

public class Product : BaseEntity<Guid>, IAuditableEntity
{
    public required string NameProduct { get; set; }
    public required string DescriptionProduct { get; set; }
    public string? ImageUrl { get; set; }
    public decimal PriceProduct { get; set; }
    
    public Guid SellerId { get; set; }
    public Seller Seller { get; set; }
    
    public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    public ICollection<OrderDetail> OrderDetails = new List<OrderDetail>();
    public ICollection<ProductCategory> ProductCategories = new List<ProductCategory>();
    public ICollection<ProductStorage> ProductStorages = new List<ProductStorage>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}