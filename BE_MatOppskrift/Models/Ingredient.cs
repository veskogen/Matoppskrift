namespace BE_MatOppskrift.Models
{
    public class Ingredient
    {
        public int Id { get; set; } //PK
        public string Name { get; set; }
        public string Quantity { get; set; } // e.g., "1 cup", "2 large"

        // Foreign key for the recipe this ingredient belongs to
        public int RecipeId { get; set; }
        // Navigation property for the recipe (many-to-one relationship)
        public Recipe Recipe { get; set; }
    }
}