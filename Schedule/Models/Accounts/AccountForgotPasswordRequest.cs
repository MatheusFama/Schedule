using System.ComponentModel.DataAnnotations;

namespace Schedule.Models.Accounts
{
    public class AccountForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
