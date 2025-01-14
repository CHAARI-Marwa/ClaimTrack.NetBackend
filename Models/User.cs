using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClaimTrack.NetBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(client|responsable)$", ErrorMessage = "Role must be either 'client' or 'responsable'.")]
        public string Role { get; set; }

        public ICollection<ArticleVendu> ArticlesVendus { get; set; }
    }
}
