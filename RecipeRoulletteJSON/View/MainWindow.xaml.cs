using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecipeRouletteJSON.Data;
using RecipeRouletteJSON.Utility;
using RecipeRouletteJSON.View;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RecipeRoulletteJSON
{
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
                SaveRecipes();
            }
        }

        private void UpdateList() {
            RecipeList.DataContext = null;
            IngredientList.DataContext = null;
            Description.Text = null;
            Instructions.DataContext = null;
            RecipeList.DataContext = data.Recipes;
        }

        private void SaveRecipes() {
            using (StreamWriter file = File.CreateText(@data.RecipeFileLocation)) {
                using (JsonTextWriter writer = new JsonTextWriter(file)) {
                    JsonSerializer json = new JsonSerializer();
                    json.Serialize(writer, data.Recipes);
                }
            }
        }

        private InternalData InitData() {
            InternalData data = new InternalData();
            Load load = new Load();

            load.Config(data);
            load.Recipes(data);

            return data;
        }

        private void BackUp() {
            string filename = data.SaveMultipleBackups ? String.Format("recipes-backup-{0}.json", DateTime.Now.ToString("yyyy-MM-dd-HHmmss")) : "recipes-backup.json";
            BackUp backup = new BackUp();
            backup.Execute(data.RecipeFileLocation, data.BackUpLocation + filename);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            AddRecipe window = new AddRecipe();
            window.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            Alert alert = new Alert();

            if (alert.DidUserConfirm()) {
                BackUp();
                DeleteRecipe();
                UpdateList();
            }
        }
    }
}
