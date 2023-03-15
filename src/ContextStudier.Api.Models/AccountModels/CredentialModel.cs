using System.ComponentModel.DataAnnotations;

namespace ContextStudier.Api.Models.AccountModels
{
    public class CredentialModel
    {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Minimum password length is 8")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}