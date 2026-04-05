using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PracticeBuildCSharp.Repository;
using PracticeBuildCSharp.Repository.Entity;

namespace PracticeBuildCSharp.Service.Cart;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    public async Task CreateCart()
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        
        var userIdGuid = Guid.Parse(userId!);
        
        var isExist = await _dbContext.Carts.AnyAsync(x => x.UserId == userIdGuid);

        if (isExist)
        {
            throw new Exception($"Cart is already exist");
        }

        var cart = new Repository.Entity.Cart()
        {
            UserId = userIdGuid,
        };
        _dbContext.Add(cart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddProductToCart(Request.AddProductToCartRequest request)
    {
       var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

       var userIdGuid = Guid.Parse(userId!);

       var cart = await _dbContext.Carts.FirstOrDefaultAsync(x => x.UserId == userIdGuid);

       if (cart == null)
       {
           cart = new Repository.Entity.Cart()
           {
               Id = Guid.NewGuid(),
               UserId = userIdGuid,
           };
           _dbContext.Add(cart);
           await _dbContext.SaveChangesAsync();
       }

       var product = _dbContext.CartDetails.Where(x => x.CartId == cart.Id && x.ProductId == request.ProductId);

       var cartExist = await product.FirstOrDefaultAsync();

       if (cartExist != null)
       {
           cartExist.Quantity += request.Quantity;
           _dbContext.Update(cartExist);
           await _dbContext.SaveChangesAsync();
           return;
       }

       var cartDetail = new CartDetail()
       {
           CartId = cart.Id,
           ProductId = request.ProductId,
           Quantity = request.Quantity
       };
       
       _dbContext.Add(cartDetail);
       await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveProductFromCart(Request.RemoveProductFromCartRequest request)
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

        var userIdGuid = Guid.Parse(userId!);
        
        var product = _dbContext.CartDetails.Where(
            x => x.Cart.UserId == userIdGuid && x.ProductId == request.ProductId);

        var cartDetail = await product.FirstOrDefaultAsync();

        if (cartDetail == null)
        {
            throw new Exception("Product not exist in cart");
        }
        _dbContext.Remove(cartDetail);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task<List<Response.ProductResponse>> GetCart()
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        
        var userIdGuid = Guid.Parse(userId!);

        var query = _dbContext.CartDetails
            .Where(x => x.Cart.UserId == userIdGuid)
            .Select(x => new Response.ProductResponse()
            {
                Name = x.Product.NameProduct,
                Description =  x.Product.DescriptionProduct,
                Price = x.Product.PriceProduct,
                Quantity = x.Quantity,
                Url =  x.Product.ImageUrl,
            });

        var results = await query.ToListAsync();

        return results;
    }
}