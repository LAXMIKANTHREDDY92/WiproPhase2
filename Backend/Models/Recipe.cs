using RecipeManagementAPI.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public string ImageUrl { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
