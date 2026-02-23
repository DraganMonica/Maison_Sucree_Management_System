using Maison_Sucree.Services.AuthAPI.Models;

namespace Maison_Sucree.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> roles);
    }
}
