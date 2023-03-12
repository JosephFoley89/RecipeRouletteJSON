using RecipeRoulletteJSON.Model;
using System.Collections.Generic;

namespace RecipeRouletteJSON.Utility {
    internal class GenerateGroceryList {
        public Dictionary<string, string> Generate(List<Recipe> recipes) {
            Dictionary<string, string> groceryList = new Dictionary<string, string>();

            foreach (Recipe recipe in recipes) {
                foreach (string key in recipe.Measurements.Keys) {
                    if (groceryList.ContainsKey(key)) {
                        groceryList[key] += recipe.Measurements[key];
                    } else {
                        groceryList.Add(key, recipe.Measurements[key]);
                    }
                }
            }

            return groceryList;
        }
    }
}
