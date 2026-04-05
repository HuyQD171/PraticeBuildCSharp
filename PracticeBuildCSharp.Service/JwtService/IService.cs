
using System.Security.Claims;

namespace PracticeBuildCSharp.Service.JwtService;

public interface IService
{
        public string GenerateJwtToken(IEnumerable<Claim> claims);
        
        ClaimsPrincipal ValidateToken(string token);
}       