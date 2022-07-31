using System.ComponentModel.DataAnnotations;

namespace Schedule.Models.Accounts
{
    public class AccountVerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
