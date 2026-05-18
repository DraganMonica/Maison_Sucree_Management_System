using System.ComponentModel.DataAnnotations;

namespace Maison_Sucree.Web.Models
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-ZăâîșțĂÂÎȘȚ\s\-]+$", ErrorMessage = "Name can only contain letters, spaces and hyphens.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(\+40|0)[0-9]{9}$", ErrorMessage = "Please enter a valid Romanian phone number (e.g. 0712345678 or +40712345678).")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
