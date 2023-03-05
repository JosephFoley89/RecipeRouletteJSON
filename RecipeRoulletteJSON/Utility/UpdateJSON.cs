using Newtonsoft.Json;
using RecipeRouletteJSON.Data;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace RecipeRouletteJSON.Utility {
    class UpdateJSON {
        private InternalData data = InitData();

        private static InternalData InitData() {
            InternalData data = new InternalData();
            Load load = new Load();

            load.Config(data);
            load.Recipes(data);

            return data;
        }

        public void BackUp() {
            string filename = data.SaveMultipleBackups ? String.Format("recipes-backup-{0}.json", DateTime.Now.ToString("yyyy-MM-dd-HHmmss")) : "recipes-backup.json";
            BackUp backup = new BackUp();
            backup.Execute(data.RecipeFileLocation, data.BackUpLocation + filename);
        }

        public void SaveRecipes(List<Recipe> recipes) {
            BackUp();
            using (StreamWriter file = File.CreateText(@data.RecipeFileLocation)) {
                using (JsonTextWriter writer = new JsonTextWriter(file)) {
                    JsonSerializer json = new JsonSerializer();
                    json.Serialize(writer, recipes);
                }
            }
        }


    }
}
