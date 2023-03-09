using RecipeRoulletteJSON.Model;
using System.Collections.Generic;

namespace RecipeRouletteJSON.ProjectData {
    class Data {
        public string FileLocation { get; set; }
        public string BackUpLocation {  get; set; }
        public bool SaveMultipleBackups { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
