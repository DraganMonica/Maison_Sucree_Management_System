using System.ComponentModel.DataAnnotations;

namespace Maison_Sucree.Services.AuthAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZăâîșțĂÂÎȘȚ\s\-]+$")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(\+40|0)[0-9]{9}$")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
