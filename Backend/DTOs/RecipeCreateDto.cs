public class RecipeCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
}
