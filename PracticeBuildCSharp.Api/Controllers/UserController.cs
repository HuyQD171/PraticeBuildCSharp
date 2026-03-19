

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PracticeBuildCSharp.Repository;
using PraticeBuildCSharp.Service.User;

namespace PracticeBuildCSharp.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _service;


    public UserController(AppDbContext dbContext, IService service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUser(string? searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        var user = await _service.GetUser(searchTerm, pageIndex, pageSize);
        return Ok(user);
    }
}