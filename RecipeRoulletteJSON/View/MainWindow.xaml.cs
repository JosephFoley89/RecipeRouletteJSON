using RecipeRouletteJSON.Model;
using RecipeRouletteJSON.ProjectData;
using RecipeRouletteJSON.Utility;
using RecipeRouletteJSON.View;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private CleanBackUp clean = new CleanBackUp();

        public MainWindow() {
            InitializeComponent();
            clean.Execute();
            data = load.LoadConfigFile();

            if (data.Recipes == null) {
                Config config = new Config();
                this.Close();
                config.Show();
            }

            data.SelectedRecipe = null;
            update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            RecipeList.DataContext = data.Recipes.OrderBy(x => x.Name).ToList();
        }
        
        private void RecipeList_SelectionChanged(object sender, EventArgs e) {
            if (RecipeList.SelectedItem != null) {
                string selection = RecipeList.SelectedItem.ToString();
                foreach (Recipe recipe in data.Recipes) {
                    if (recipe.Name == selection) {
                        data.SelectedRecipe = recipe;
                        selectedRecipe = recipe;
                        update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);

                        SetIngredientList();
                        SetInstructions();
                        SetDescription();
                        break;
                    }
                }
            } else {
                data.SelectedRecipe = null;
                update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);
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

        private void UpdateList(List<Recipe> recipes) {
            RecipeList.DataContext = null;
            IngredientList.DataContext = null;
            Description.Text = null;
            Instructions.DataContext = null;
            RecipeList.DataContext = recipes;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            AddRecipe window = new AddRecipe();
            this.Close();
            window.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            Alert alert = new Alert();
            if (selectedRecipe != null) {
                if (alert.DidUserConfirm(String.Format("delete the {0} recipe", RecipeList.SelectedItem.ToString()))) {
                    DeleteRecipe();
                    UpdateList(data.Recipes);
                }
            } else {
                alert.DisplayError("Please select a recipe to remove.", "No Recipe Selected");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (selectedRecipe != null) {
                Notification notification = new Notification(data);
                notification.SendInstructions(selectedRecipe);
            } else {
                Alert alert = new Alert();
                alert.DisplayError("Please select a recipe to have emailed to you.", "No Recipe Selected");
            }
        }

        private void Searchbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            List<Recipe> recipes = new List<Recipe>();

            foreach(Recipe recipe in data.Recipes) {
                if (recipe.Name.ToLower().Contains(Searchbox.Text.ToLower())) {
                    recipes.Add(recipe);
                }
            }

            UpdateList(recipes);
        }

        private void ConfigButton_Click(object sender, RoutedEventArgs e) {
            Config config = new Config();
            config.Show();
            this.Close();
        }

        private void MealPlanButton_Click(object sender, RoutedEventArgs e) {
            MealPlan plan = new MealPlan();
            plan.Show();
            this.Close();
        }
    }
}
