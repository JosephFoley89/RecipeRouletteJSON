using RecipeRouletteJSON.ProjectData;

namespace RecipeRouletteJSON.Model {
    internal class Ingredient {
        public int Id { get; set; }
        public string Name { get; set; }
        public FoodGroup FoodGroup { get; set; }
    }
}
