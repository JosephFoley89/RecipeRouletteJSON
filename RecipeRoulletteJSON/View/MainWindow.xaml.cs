using RecipeRouletteJSON.Model;
using RecipeRouletteJSON.ProjectData;
using RecipeRouletteJSON.Utility;
using RecipeRouletteJSON.View;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RecipeRoulletteJSON {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Data data;
        private Load load = new Load();
        private Recipe selectedRecipe;
        private UpdateJSON update = new UpdateJSON();
        private GenerateMealPlan plan = new GenerateMealPlan();

        public MainWindow() {
            InitializeComponent();
            data = load.LoadConfigFile();

            if (data.Recipes == null) {
                Config config = new Config();
                this.Close();
                config.Show();
            }

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            RecipeList.DataContext = data.Recipes;
        }
        
        private void RecipeList_SelectionChanged(object sender, EventArgs e) {
            if (RecipeList.SelectedItem != null) {
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

        private void DeleteRecipe() {
            if (selectedRecipe != null) {
                data.Recipes.Remove(selectedRecipe);
                update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);
            }
        }

        private void UpdateList() {
            RecipeList.DataContext = null;
            IngredientList.DataContext = null;
            Description.Text = null;
            Instructions.DataContext = null;
            RecipeList.DataContext = data.Recipes;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            AddRecipe window = new AddRecipe();
            this.Close();
            window.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            Alert alert = new Alert();

            if (alert.DidUserConfirm()) {
                DeleteRecipe();
                UpdateList();
            }
        }

        //TODO: Fully implement the meal plan functionality
        private void GeneratePlanButton_Click(object sender, RoutedEventArgs e) {
            List<Recipe> recipes = plan.Generate();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (selectedRecipe != null) {
                Notification notification = new Notification();
                notification.SendInstructions(selectedRecipe);
            }
        }
    }
}
