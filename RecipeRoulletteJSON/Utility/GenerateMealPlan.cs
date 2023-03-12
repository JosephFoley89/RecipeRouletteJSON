using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;

namespace RecipeRouletteJSON.Utility {
    internal class GenerateMealPlan {
        public List<Recipe> Generate() {
            Load load = new Load();
            Data data = load.LoadConfigFile();
            Notification notification = new Notification();
            List<Recipe> mealPlan = new List<Recipe>();

            for (int i = 0; i < 7; i++) {
                Random random = new Random();
                int index = random.Next(0, data.Recipes.Count);
                mealPlan.Add(data.Recipes[index]);
            }

            notification.SendShoppingList(mealPlan);

            return mealPlan;
        }
    }
}
