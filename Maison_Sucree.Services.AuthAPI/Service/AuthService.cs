using Microsoft.AspNetCore.Identity;
using Maison_Sucree.Services.AuthAPI.Models;
using Maison_Sucree.Services.AuthAPI.Models.Dto;
using Maison_Sucree.Services.AuthAPI.Service.IService;
using Maison_Sucree.Services.AuthAPI.Data;

namespace Maison_Sucree.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
             UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // creez role daca nu exista
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            //var user = await _userManager.FindByNameAsync(loginRequestDto.UserName);
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
           
            if (user == null|| isValid==false)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = ""
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token=_jwtTokenGenerator.GenerateToken(user, roles);

            //  daca a fost gasit user ul, generez jwt token
            UserDto userDto = new()
            {
                ID = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token,
                
            };

            return loginResponseDto;
        }
        


        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                //                                                se asteapta parola aici
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("CUSTOMER").GetAwaiter().GetResult())
                        _roleManager.CreateAsync(new IdentityRole("CUSTOMER")).GetAwaiter().GetResult();

                    await _userManager.AddToRoleAsync(user, "CUSTOMER");

                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        ID = user.Id,
                        Email = userToReturn.Email,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
