using Newtonsoft.Json;
using RecipeRouletteJSON.ProjectData;
using System.IO;

namespace RecipeRoulletteJSON.Utility {
    class Load {
        public Data LoadConfigFile() {
            if (Directory.Exists(@"C:\RecipeRoulette")) {
                if (File.Exists(@"C:\RecipeRoulette\config.json")) {
                    using (StreamReader file = new StreamReader(@"C:\RecipeRoulette\config.json")) {
                        string json = file.ReadToEnd();
                        Data data = JsonConvert.DeserializeObject<Data>(json);
                        return data;
                    }
                } else {
                    Data data = new Data();
                    return data;
                }
            } else {
                Directory.CreateDirectory(@"C:\RecipeRoulette");
                Directory.CreateDirectory(@"C:\RecipeRoulette\BackUps");

                Data data = new Data();
                return data;
            }
        }
    }
}
