using System;
using System.ComponentModel.DataAnnotations;

namespace SchAppAPI.DOA
{
    public class ForgotPassword
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
