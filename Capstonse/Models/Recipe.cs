using System.ComponentModel.DataAnnotations;

namespace RecipeManagementAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
