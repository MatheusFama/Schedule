using System.ComponentModel.DataAnnotations;

namespace Schedule.Models.Accounts
{
    public class AccountAuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
