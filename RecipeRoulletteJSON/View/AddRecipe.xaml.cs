using RecipeRouletteJSON.Data;
using RecipeRouletteJSON.Utility;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipeRouletteJSON.View {
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window {
        private List<string> types = new List<string>();
        private List<string> instructions = new List<string> ();
        private List<string> ingredients = new List<string>();
        private Dictionary<string, string> measurements = new Dictionary<string, string>();
        private Alert alert = new Alert();
        private InternalData data;
        private UpdateJSON update = new UpdateJSON();

        public AddRecipe() {
            InitializeComponent();
            data = InitData();

            DifficulySelect.DataContext = Enum.GetValues (typeof (Difficulty));
        }

        private InternalData InitData() {
            InternalData data = new InternalData();
            Load load = new Load();

            load.Config(data);
            load.Recipes(data);

            return data;
        }

        private void UpdateListBoxes() {
            ActiveIngredients.DataContext = null;
            ActiveInstructions.DataContext = null;
            ActiveTypes.DataContext = null;

            ActiveIngredients.DataContext = AssembleListOfMeasurements();
            ActiveInstructions.DataContext = instructions;
            ActiveTypes.DataContext = types;
        }

        private void CleanInputs() {
            RecipeName.Text = string.Empty;
            Description.Text = string.Empty;
            Cuisine.Text = string.Empty;
            Type.Text = string.Empty;
            Ingredient.Text = string.Empty;
            Amount.Text = string.Empty;
            Instruction.Text = string.Empty;
            DifficulySelect.SelectedIndex = 0;
            types.Clear();
            instructions.Clear();
            measurements.Clear();
            ingredients.Clear();
        }

        private List<string> AssembleListOfMeasurements() {
            List<string> formattedIngredients = new List<string>();

            foreach(string key in measurements.Keys) {
                string formattedValue = String.Format("{0}: {1}", key, measurements[key]);
                formattedIngredients.Add(formattedValue);
            }

            ingredients = formattedIngredients;

            return formattedIngredients;
        }

        private void AddInstructionButton_Click(object sender, RoutedEventArgs e) {
            instructions.Add(Instruction.Text);
            Instruction.Text = string.Empty;
            UpdateListBoxes();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            if (alert.DidUserConfirm()) {
                CleanInputs();
                UpdateListBoxes();
            }
        }

        private void AddTypeButton_Click(object sender, RoutedEventArgs e) {
            types.Add(Type.Text);
            Type.Text = string.Empty;
            UpdateListBoxes();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e) {
            //TODO: Check for key first.
            measurements.Add(Ingredient.Text, Amount.Text);
            Ingredient.Text = string.Empty;
            Amount.Text = string.Empty;
            UpdateListBoxes();
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e) {
            if (alert.DidUserConfirm()) {
                Recipe recipe = new Recipe(
                    data.Recipes.Count + 1,
                    RecipeName.Text,
                    Description.Text,
                    Cuisine.Text,
                    types,
                    measurements,
                    instructions,
                    (Difficulty)DifficulySelect.SelectedValue
                );

                data.Recipes.Add(recipe);
                update.SaveRecipes(data.Recipes);
                CleanInputs();
                UpdateListBoxes();
            }
        }
    }
}
