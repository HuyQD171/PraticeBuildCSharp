using Microsoft.EntityFrameworkCore;
using PracticeBuildCSharp.Repository;

namespace PraticeBuildCSharp.Service.Seller;

public class Service: IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Base.Response.PageResult<Response.GetSellerResponse>> GetAllSeller(string? searchTerm, int pageIndex, int pageSize)
    {
        var query = _dbContext.Sellers.Where(x => true);

        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.User.FirstName.Contains(searchTerm) 
                || x.User.LastName.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.User.FirstName);
        
        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        var selectQuery = query.Select(x => new Response.GetSellerResponse()
        {
            Id = x.Id,
            Email = x.User.Email,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Role = x.User.Role,
            CompanyName = x.CompanyName,
            TaxCode =  x.TaxCode
        });

        var listResult = await selectQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new Base.Response.PageResult<Response.GetSellerResponse>()
        {
            Items = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };

        return result;
    }
}