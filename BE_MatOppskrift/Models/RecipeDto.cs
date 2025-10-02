
using System.Collections.Generic; // For collection types
using System.ComponentModel.DataAnnotations; // For data validation attributes

namespace BE_MatOppskrift.Models
{
//////////////For POST////////////////
    public class RecipeCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public List<IngredientCreateDto> Ingredients { get; set; }

        [Range(1, 1440)] // Minutes in a day
        public int PrepTimeMinutes { get; set; } = 30;

        public int CookTimeMinutes { get; set; } = 30;
    }

    public class IngredientCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }
    }

//////////////For GET////////////////
    public class RecipeReadDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public List<IngredientReadDto> Ingredients { get; set; }

        [Range(1, 1440)] // Minutes in a day
        public int PrepTimeMinutes { get; set; } = 30;

        public int CookTimeMinutes { get; set; } = 30;
    }



    public class IngredientReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

}