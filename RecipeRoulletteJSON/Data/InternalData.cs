using RecipeRoulletteJSON.Model;
using System.Collections.Generic;

namespace RecipeRouletteJSON.Data {
    class InternalData {
        public string RecipeFileLocation { get; set; }
        public string BackUpLocation { get; set; }
        public bool SaveMultipleBackups { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
