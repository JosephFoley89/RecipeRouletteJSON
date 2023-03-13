using RecipeRouletteJSON.ProjectData;
using RecipeRouletteJSON.Utility;
using RecipeRoulletteJSON;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RecipeRouletteJSON.View {
    /// <summary>
    /// Interaction logic for MealPlan.xaml
    /// </summary>
    public partial class MealPlan : Window {
        private Data data;
        private static Load load = new Load();
        private Notification notification = new Notification(load.LoadConfigFile());
        private Alert alert = new Alert();
        private bool hasSaved = true;
        private UpdateJSON update = new UpdateJSON();

        public MealPlan() {
            InitializeComponent();
            data = load.LoadConfigFile();
            AvailableRecipesList.DataContext = data.Recipes.OrderBy(x => Name);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            if (data.MealPlan.Count > 0 ) {
                MealPlanList.DataContext = data.MealPlan;
            }
        }

        private void SavePlanButton_Click(object sender, RoutedEventArgs e) {
            if (hasSaved) {
                alert.DisplayError("No changes to save.", "No changes since last save.");
            } else {
                List<Recipe> recipes = new List<Recipe>();

                foreach (Recipe recipe in MealPlanList.Items) {
                    recipes.Add(recipe);
                }

                data.MealPlan = recipes;
                alert.DisplayError("Your changes have been saved.", "Data Saved");
                hasSaved = true;

                update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);
            }
        }

        private void EmailIngredientsButton_Click(object sender, RoutedEventArgs e) {
            notification.SendShoppingList(data.MealPlan);
        }

        private void EmailInstructions_Click(object sender, RoutedEventArgs e) {
            foreach(Recipe recipe in data.MealPlan) {
                if (recipe.Name == MealPlanList.SelectedItem.ToString()) {
                    notification.SendInstructions(recipe);
                    break;
                }
            }
        }
        
        private void UpdateList() {
            MealPlanList.DataContext = null;
            MealPlanList.DataContext = data.MealPlan;
        }

        private void MealPlanList_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Delete) {
                data.MealPlan.Remove((Recipe)MealPlanList.SelectedItem);
                hasSaved = false;
                UpdateList();
            }

        }

        private void AvailableRecipesList_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                data.MealPlan.Add((Recipe)AvailableRecipesList.SelectedItem);
                hasSaved = false;
                UpdateList();
            }
        }

        private void MealPlan_Closing(object sender, EventArgs e) {
            List<Recipe> mealPlan = new List<Recipe>();

            foreach(Recipe recipe in MealPlanList.Items) {
                mealPlan.Add((Recipe)recipe);
            }

            if (!hasSaved) {
                if (alert.DidUserConfirm("close the window and revert any unsaved changes")) {
                    this.Hide();
                    MainWindow window = new MainWindow();
                    window.ShowDialog();
                }
            } else {
                this.Hide();
                MainWindow window = new MainWindow();
                window.ShowDialog();
            }
        }
    }
}
