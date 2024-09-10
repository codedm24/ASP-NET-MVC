using System.ComponentModel.DataAnnotations;

namespace DemoWebsite4.Models
{
    public class ForgotPasswordViewModel
    {
        public int Id {  get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
