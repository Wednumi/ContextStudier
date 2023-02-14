using ContextStudier.Api.Models.MapperConfig;
using System.ComponentModel.DataAnnotations;

namespace ContextStudier.Api.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        [NoMap]
        [Required]
        [MinLength(8, ErrorMessage = "Minimum password length is 8")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NoMap]
        [Required]
        [MinLength(8, ErrorMessage = "Minimum password length is 8")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}