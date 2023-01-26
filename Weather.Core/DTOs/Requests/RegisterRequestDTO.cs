namespace Weather.Core.Requests
{
    public class RegisterRequestDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The password field {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "The password field must have capital & small letter, number and special character")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}