
using Microsoft.EntityFrameworkCore;
using PracticeBuildCSharp.Repository.Entity;

namespace PracticeBuildCSharp.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}

    public static Guid CateGoryParentId1 = Guid.NewGuid();
    public static Guid CateGoryParentId2 = Guid.NewGuid();

    
    public DbSet<User> Users { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductStorage> ProductStorages { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var listUsersId = new List<Guid>();
        var listSellersId = new List<Guid>();
        var listOrdersId = new List<Guid>();
        var listProductsId = new List<Guid>();
        var listStoragesId = new List<Guid>();
        var listCategoriesId = new List<Guid>();


        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(x => x.HashPassword)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("User");

            builder.HasOne(x => x.Seller)
                .WithOne(x => x.User)
                .HasForeignKey<Seller>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            List<User> users = new List<User>();
            for (int i = 0; i < 50; i++)
            {
                var newUser = new User()
                {
                    FirstName = $"Tuc{i}",
                    LastName = $"Huynh{i}",
                    Email = $"TrucHuynh{i}@gmail.com",
                    Id = Guid.NewGuid(),
                    HashPassword = $"hash_password{i}"
                };
                listUsersId.Add(newUser.Id);
                users.Add(newUser);
            }

            builder.HasData(users);

        });

        modelBuilder.Entity<Seller>(builder =>
        {
            builder.Property(x => x.CompanyName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.CompanyAddress)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.TaxCode)
                .HasMaxLength(200)
                .IsRequired();
            List<Seller> sellers = new List<Seller>();
            for (int i = 0; i < listUsersId.Count - 1; i++)
            {
                var newSeller = new Seller()
                {
                    Id = Guid.NewGuid(),
                    CompanyAddress = "CompanyAddress" + i,
                    CompanyName = "CompanyName" + i,
                    TaxCode = "SE8428" + i,
                    UserId = listUsersId[i]
                };
                listSellersId.Add(newSeller.Id);
                sellers.Add(newSeller);
            }

            builder.HasData(sellers);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.Property(x => x.NameProduct)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.PriceProduct)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(x => x.DescriptionProduct)
                .HasMaxLength(200)
                .IsRequired();
            List<Product> products = new List<Product>();
            for (int i = 0; i < 50; i++)
            {
                var newProduct = new Product()
                {
                    Id = Guid.NewGuid(),
                    NameProduct = $"ProductName{i}",
                    PriceProduct = 9900M,
                    DescriptionProduct = $"DescriptionProduct{i}",
                };
                listProductsId.Add(newProduct.Id);
                products.Add(newProduct);
            }

            builder.HasData(products);
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.Property(x => x.TotalAmount)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Address)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Status)
                .HasMaxLength(200)
                .IsRequired();
            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            List<Order> orders = new List<Order>();
            for (int i = 0; i < 50; i++)
            {
                var newOrder = new Order()
                {
                    Id = Guid.NewGuid(),
                    TotalAmount = i,
                    Address = $"OrderAddress{i}",
                    Status = "OrderStatus" + i,
                    UserId = listUsersId[i]
                };
                listOrdersId.Add(newOrder.Id);
                orders.Add(newOrder);
            }

            builder.HasData(orders);
        });
        modelBuilder.Entity<OrderDetail>(builder =>
        {
            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            List<OrderDetail> ordersDetail = new List<OrderDetail>();
            for (int i = 0; i < listProductsId.Count - 1; i++)
            {
                var newOrderDetail = new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    UnitPrice = 100M,
                    Quantity = i,
                    ProductId = listProductsId[i],
                    OrderId = listOrdersId[i]
                };
                ordersDetail.Add(newOrderDetail);
            }

            builder.HasData(ordersDetail);
        });
        modelBuilder.Entity<Storage>(builder =>
        {
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(x => x.Type)
                .HasMaxLength(50)
                .IsRequired();
            List<Storage> storages = new List<Storage>();
            for (int i = 0; i < 50; i++)
            {
                var newStorage = new Storage()
                {
                    Id = Guid.NewGuid(),
                    Price = i * 2300M,
                    Type = $"Storage{i}",
                };
                listStoragesId.Add(newStorage.Id);
                storages.Add(newStorage);
            }

            builder.HasData(storages);
        });
        modelBuilder.Entity<ProductStorage>(builder =>
        {
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductStorages)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Storage)
                .WithMany(x => x.ProductStorages)
                .HasForeignKey(x => x.StorageId)
                .OnDelete(DeleteBehavior.Cascade);
            List<ProductStorage> productStorages = new List<ProductStorage>();
            for (int i = 0; i < listProductsId.Count - 1; i++)
            {
                var newProductStorage = new ProductStorage()
                {
                    Id = Guid.NewGuid(),
                    ProductId = listProductsId[i],
                    StorageId = listStoragesId[i]
                };
                productStorages.Add(newProductStorage);
            }

            builder.HasData(productStorages);
        });
        modelBuilder.Entity<Category>(builder =>
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            var categories = new List<Category>()
            {
                new()
                {
                    Id = CateGoryParentId1,
                    Name = "Áo",
                },
                new()
                {
                    Id = CateGoryParentId2,
                    Name = "Quần",
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo thể thao",
                    ParentId = CateGoryParentId1
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo ba lỗ",
                    ParentId = CateGoryParentId1
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Quần Jean",
                    ParentId = CateGoryParentId2
                },
            };
            for (int i = 0; i <= 50; i++)
            {
                if (i % 2 == 0)
                {
                    var category = new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Áo ba lỗ mã" + i,
                        ParentId = CateGoryParentId1
                    };
                    listCategoriesId.Add(category.Id);
                    categories.Add(category);
                }
                else
                {
                    var category = new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Quần Jean mã " + i,
                        ParentId = CateGoryParentId2
                    };
                    listCategoriesId.Add(category.Id);
                    categories.Add(category);
                }

            }
            
            builder.HasData(categories);
        });

            modelBuilder.Entity<ProductCategory>(builder =>
            {
                builder.HasOne(x => x.Product)
                    .WithMany(x => x.ProductCategories)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(x => x.Category)
                    .WithMany(x => x.ProductCategories)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                List<ProductCategory> productCategory = new List<ProductCategory>();
                for (int i = 0; i < listProductsId.Count - 1; i++)
                {
                    var newProCategory = new ProductCategory()
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = listCategoriesId[i],
                        ProductId = listProductsId[i],
                    };
                    productCategory.Add(newProCategory);
                }
                builder.HasData(productCategory);
            });
            modelBuilder.Entity<Inventory>(builder =>
            {
                builder.Property(x => x.TotalInStock)
                    .HasMaxLength(200)
                    .IsRequired();
                builder.Property(x => x.TotalSell)
                    .HasMaxLength(200)
                    .IsRequired();
                List<Inventory> inventories = new List<Inventory>();
                for (int i = 0; i < listProductsId.Count - 1; i++)
                {
                    var newInvent = new Inventory()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = listProductsId[i],
                        TotalInStock = i * 54,
                        TotalSell = 1 + i * 23,
                    };
                    inventories.Add(newInvent);
                }
                builder.HasData(inventories);
            });
            modelBuilder.Entity<Cart>(builder =>
            {
                List<Cart> carts = new List<Cart>();
                for (int i = 0; i < listUsersId.Count - 1; i++)
                {
                    var newCart = new Cart()
                    {
                        Id = Guid.NewGuid(),
                        UserId = listUsersId[i],
                    };
                    carts.Add(newCart);
                }
                builder.HasData(carts);
            });

    }
}