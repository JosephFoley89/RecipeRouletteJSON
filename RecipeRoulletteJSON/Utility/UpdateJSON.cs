using Newtonsoft.Json;
using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RecipeRouletteJSON.Utility {
    class UpdateJSON {
        public void BackUp(string source, string destination, bool saveMultiple) {
            string filename = saveMultiple ? String.Format("recipes-backup-{0}.json", DateTime.Now.ToString("yyyyMMddHHmmss")) : "recipes-backup.json";
            BackUp backup = new BackUp();
            backup.Execute(@source, destination + filename);
        }

        public void SaveRecipes(Data data, string source, string destination, bool saveMultiple) {
            BackUp(source, destination, saveMultiple);
            using (StreamWriter file = File.CreateText(@source)) {
                using (JsonTextWriter writer = new JsonTextWriter(file)) {
                    JsonSerializer json = new JsonSerializer();
                    json.Serialize(writer, data);
                }
            }
        }
    }
}
