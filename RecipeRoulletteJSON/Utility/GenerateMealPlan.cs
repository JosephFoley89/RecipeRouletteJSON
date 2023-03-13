using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;

namespace RecipeRouletteJSON.Utility {
    internal class GenerateMealPlan {
        public void Generate() {
            Load load = new Load();
            Data data = load.LoadConfigFile();
            UpdateJSON update = new UpdateJSON();
            List<Recipe> mealPlan = new List<Recipe>();

            for (int i = 0; i < 7; i++) {
                Random random = new Random();
                int index = random.Next(0, data.Recipes.Count);
                if (data.Recipes[index].Course.Contains("Main")) {
                    mealPlan.Add(data.Recipes[index]);
                } else {
                    i--;
                }
            }

            data.MealPlan = mealPlan;
            update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);
        }
    }
}
