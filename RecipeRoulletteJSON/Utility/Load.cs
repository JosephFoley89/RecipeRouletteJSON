using Newtonsoft.Json;
using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Model;
using System.Collections.Generic;
using System.IO;

namespace RecipeRoulletteJSON.Utility
{
    class Load {
        public Data LoadConfigFile() {
            using (StreamReader file = new StreamReader(@"C:\RecipeRoulette\config.json")) {
                string json = file.ReadToEnd();
                Data data = JsonConvert.DeserializeObject<Data>(json);
                return data;
            }
        }
    }
}
