using RecipeRoulletteJSON.Data;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeRoulletteJSON {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private InternalData data;
        private Recipe selectedRecipe;

        public MainWindow() {
            InitializeComponent();
            data = InitData();

            RecipeList.DataContext = data.Recipes;
        }
        
        private void RecipeList_SelectionChanged(object sender, EventArgs e) {
            string selection = RecipeList.SelectedItem.ToString();

            foreach (Recipe recipe in data.Recipes) {
                if (recipe.Name == selection) {
                    selectedRecipe = recipe;

                    SetIngredientList();
                    SetInstructions();
                    SetDescription();
                    break;
                }
            }

        }

        private void SetIngredientList() {
            Dictionary<string, string> map = selectedRecipe.Measurements;
            List<string> ingredients = new List<string>();

            foreach (string key in map.Keys) {
                ingredients.Add(String.Format("{0}: {1}", key, map[key]));
            }

            IngredientList.DataContext = ingredients;
        }

        private void SetInstructions() {
            Instructions.DataContext = selectedRecipe.Directions;
        }

        private void SetDescription() {
            Description.Text = selectedRecipe.Description;
        }

        private InternalData InitData() {
            InternalData data = new InternalData();
            Load load = new Load();

            load.Config(data);
            load.Recipes(data);

            return data;
        }

        private void ShowOff(Recipe recipe) {
            MessageBox.Show(
                recipe.Description,
                recipe.Name
            );
        }
    }
}
