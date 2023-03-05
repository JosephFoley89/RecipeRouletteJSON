using Newtonsoft.Json;
using RecipeRouletteJSON.Data;
using RecipeRoulletteJSON.Model;
using System.Collections.Generic;
using System.IO;

namespace RecipeRoulletteJSON.Utility
{
    class Load {
        public void Config(InternalData data) {
            using (StreamReader reader = new StreamReader(@"C:\RecipeRoulette\config.txt")) {
                Dictionary<string, string> configs = new Dictionary<string, string>();
                string line = string.Empty;

                while ((line = reader.ReadLine()) != null) {
                    if (!line.StartsWith("#")) {
                        string[] lines = line.Split("::");
                        configs.Add(lines[0], lines[1]);
                    }
                }

                data.RecipeFileLocation = configs["RECIPE_JSON"];
                data.BackUpLocation = configs["BACKUP_LOCATION"];
                data.SaveMultipleBackups = configs["SAVE_MULTIPLE_BACKUPS"].ToLower().Contains("true");
            }
        }

        public void Recipes(InternalData data) {
            using (StreamReader file = new StreamReader(data.RecipeFileLocation)) {
                string json = file.ReadToEnd();
                List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);
                data.Recipes = recipes;
            }
        }
    }
}
