using RecipeRoulletteJSON.Model;
using System.Collections.Generic;

namespace RecipeRouletteJSON.ProjectData {
    class Data {
        public string FileLocation { get; set; }
        public string BackUpLocation {  get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string SMTPKey { get; set; }
        public int Port { get; set; }
        public string ToEmail { get; set; }
        public bool SaveMultipleBackups { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Recipe> MealPlan { get; set; }
        public Recipe SelectedRecipe { get; set; }
    }
}
