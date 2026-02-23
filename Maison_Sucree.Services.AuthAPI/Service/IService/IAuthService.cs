using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Maison_Sucree.Services.AuthAPI.Models.Dto;

namespace Maison_Sucree.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
