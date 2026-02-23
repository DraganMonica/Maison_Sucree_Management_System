using Microsoft.AspNetCore.Identity;

namespace Maison_Sucree.Services.AuthAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
