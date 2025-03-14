using System.ComponentModel.DataAnnotations;

namespace RecipeManagementAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Ensure auto-increment in DB schema

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User";
    }
}
