using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRouletteJSON.Utility {
    internal class CleanBackUp {
        private Load load = new Load();
        private Data data;
        public void Execute() {
            data = load.LoadConfigFile();

            if (data.SaveMultipleBackups) {
                string[] files = Directory.GetFiles(@"C:\RecipeRoulette\BackUps", "*.json", SearchOption.TopDirectoryOnly);

                foreach (string file in files) {
                    if (File.GetCreationTime(file) < DateTime.Now.AddDays(-30)) {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}
