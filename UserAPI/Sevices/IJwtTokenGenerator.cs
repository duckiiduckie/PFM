using UserAPI.Models;

namespace UserAPI.Sevices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
