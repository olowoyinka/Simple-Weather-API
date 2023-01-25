using System.ComponentModel.DataAnnotations;

namespace Weather.Core.Requests
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}