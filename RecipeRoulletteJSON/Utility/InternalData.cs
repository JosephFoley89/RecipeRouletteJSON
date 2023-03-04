using RecipeRoulletteJSON.Model;
using System.Collections.Generic;

namespace RecipeRoulletteJSON.Data {
    class InternalData {
        public string RecipeFileLocation { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
