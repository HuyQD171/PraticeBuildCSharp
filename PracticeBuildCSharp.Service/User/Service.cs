using Microsoft.EntityFrameworkCore;
using PracticeBuildCSharp.Repository;

namespace PraticeBuildCSharp.Service.User;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
        
    public  Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Base.Response.PageResult<Response.GetUserResponse>> GetUser(string? searchTerm, int pageIndex,
        int pageSize)
    {
        var query = _dbContext.Users.Where(x => true);

        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(searchTerm)
                || x.LastName.Contains(searchTerm)
                || x.Email.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.Email);

        query = query.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var selectedQuery = query.Select(x => new Response.GetUserResponse()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Address = x.Address,
            Role = x.Role
        });

        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new Base.Response.PageResult<Response.GetUserResponse>()
        {
            Items = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
        return result;
    }

}