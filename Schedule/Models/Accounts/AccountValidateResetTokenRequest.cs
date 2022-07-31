using System.ComponentModel.DataAnnotations;

namespace Schedule.Models.Accounts
{
    public class AccountValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
