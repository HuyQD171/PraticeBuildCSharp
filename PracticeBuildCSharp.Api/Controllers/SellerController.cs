using Microsoft.AspNetCore.Mvc;
using PracticeBuildCSharp.Repository;
using PraticeBuildCSharp.Service.Seller;


namespace PracticeBuildCSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SellerController: ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _service;

    public SellerController(AppDbContext dbContext, IService service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetSellers(string? searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        var seller = await _service.GetAllSeller(searchTerm, pageIndex, pageSize);
        return Ok(seller);
    }
}