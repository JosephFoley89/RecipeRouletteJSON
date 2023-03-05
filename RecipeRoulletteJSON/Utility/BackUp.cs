using System.IO;

namespace RecipeRouletteJSON.Utility {
    internal class BackUp {
        public void Execute(string source, string destination) {
            File.Copy(source, destination, true);
        }
    }
}
