﻿using System.ComponentModel.DataAnnotations;

namespace DemoWebsite4.Models
{
    public class ResetPasswordViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100,ErrorMessage ="The {0} must be at least {2} and at max {1} characters long.",  MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

    }
}
