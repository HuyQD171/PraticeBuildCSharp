namespace PraticeBuildCSharp.Service.Identity;

public interface IService
{
    public Task<Response.IdentityResponse> login (string email, string password);
}