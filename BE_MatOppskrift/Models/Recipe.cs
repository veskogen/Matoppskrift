using System.Collections.Generic; //For list <ingredient>

namespace BE_MatOppskrift.Models
{
    public class Recipe
    {
        public int Id { get; set; } //PK
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PrepTimeMinutes { get; set; }
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }

        // Navigation property for ingredients (one-to-many relationship)
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    }
}