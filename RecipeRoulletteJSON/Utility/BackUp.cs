using System.IO;

namespace RecipeRouletteJSON.Utility {
    internal class BackUp {
        public void Execute(string source, string destination) {
            if (File.Exists(source)) {
                File.Copy(source, destination, true);
            }
        }
    }
}
