using System.ComponentModel.DataAnnotations;

namespace CleanApi.Shared.DTOs.Identity.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}