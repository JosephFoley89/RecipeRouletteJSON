using RecipeRouletteJSON.Model;
using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RecipeRouletteJSON.Utility {
    internal class GenerateHTML {
        public string GenerateShoppingList(List<Recipe> recipes) {
            string html = "<html>";
            html += "<style>"
                + "td { border-bottom: 2px solid #fefefe; }"
                + "table { width: 100%; }"
                + "</style>";

            foreach (Recipe recipe in recipes) {
                html += string.Format("<br/><table><th colspan='3'>{0}</td></th>",  recipe.Name);
                foreach (string ingredient in recipe.Measurements.Keys) {
                    html += string.Format("<tr><td>{0}</td><td>:</td><td>{1}</td></tr>", ingredient, recipe.Measurements[ingredient]);
                }
                html += "</table><br/>";
            }

            html += "</html>";
            return html;
        }

        public string GenerateInstructions(Recipe recipe) {
            int i = 1;
            string html = "<html>";
            html += "<style>"
                + "table { width: 100%; }"
                + "</style>";
            html += string.Format("<table><th colspan='3'>{0}</td></th>", recipe.Name);

            foreach (string instruction in recipe.Directions) {
                html += string.Format("<tr><td>{0}</td><td>:</td><td>{1}</td></tr>", i.ToString(), instruction);
                i++;
            }

            html += "</table></html>";
            return html;
        }
    }
}
