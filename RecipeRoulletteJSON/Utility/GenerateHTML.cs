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
                + "</style>";

            foreach (Recipe recipe in recipes) {
                html += string.Format("<table><th colspan='3'>{0}</td></th>",  recipe.Name);
                foreach (string ingredient in recipe.Measurements.Keys) {
                    html += string.Format("<tr><td>{0}</td><td>:</td><td>{1}</td></tr>", ingredient, recipe.Measurements[ingredient]);
                }
                html += "</table>";
            }

            html += "</html>";
            return html;
        }

        public string GenerateInstructions(List<Recipe> recipes) {
            string html = "<html>";


            html += "</html>";
            return html;
        }
    }
}
