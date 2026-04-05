using Microsoft.EntityFrameworkCore;
using PracticeBuildCSharp.Api.Extensions;
using PracticeBuildCSharp.Api.Middlewares;
using PracticeBuildCSharp.Repository;
using UserService = PracticeBuildCSharp.Service.User;
using SellerService = PracticeBuildCSharp.Service.Seller;
using IdentityService = PracticeBuildCSharp.Service.Identity;
using JwtService = PracticeBuildCSharp.Service.JwtService;
using CartService = PracticeBuildCSharp.Service.Cart;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();

builder.Services.AddScoped<CartService.IService, CartService.Service>();
builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<IdentityService.IService, IdentityService.Service>();
builder.Services.AddScoped<SellerService.IService, SellerService.Service>();
builder.Services.AddScoped<UserService.IService, UserService.Service>();


builder.Services.AddTransient<GlobalExceptionHandlerMiddlewares>();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddlewares>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();