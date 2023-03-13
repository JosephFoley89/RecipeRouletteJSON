using RecipeRouletteJSON.ProjectData;
using RecipeRouletteJSON.Utility;
using RecipeRoulletteJSON;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RecipeRouletteJSON.View {
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window {
        private Data data;
        private List<string> types = new List<string>();
        private List<string> instructions = new List<string> ();
        private List<string> ingredients = new List<string>();
        private Dictionary<string, string> measurements = new Dictionary<string, string>();
        private Alert alert = new Alert();
        private Load load = new Load();
        private UpdateJSON update = new UpdateJSON();

        public AddRecipe() {
            InitializeComponent();
            data = load.LoadConfigFile();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DifficulySelect.DataContext = Enum.GetValues (typeof (Difficulty));
            CuisineSelect.DataContext = Enum.GetValues (typeof (Cuisine));

            if (data.SelectedRecipe != null) {
                PopulateFields();
            }
        }

        private void PopulateFields() {
            measurements = data.SelectedRecipe.Measurements;
            instructions = data.SelectedRecipe.Directions;
            types = data.SelectedRecipe.Course;

            ActiveIngredients.DataContext = AssembleListOfMeasurements();
            ActiveInstructions.DataContext = instructions;
            ActiveTypes.DataContext = types;
            DifficulySelect.SelectedItem = data.SelectedRecipe.Difficulty;
            CuisineSelect.SelectedItem = data.SelectedRecipe.Cuisine;
            RecipeName.Text = data.SelectedRecipe.Name;
            Description.Text = data.SelectedRecipe.Description;
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
            Type.Text = string.Empty;
            Ingredient.Text = string.Empty;
            Amount.Text = string.Empty;
            Instruction.Text = string.Empty;
            DifficulySelect.SelectedIndex = 0;
            CuisineSelect.SelectedIndex = 0;
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
        //TODO: Form validation.
        private void AddInstructionButton_Click(object sender, RoutedEventArgs e) {
            instructions.Add(Instruction.Text);
            Instruction.Text = string.Empty;
            UpdateListBoxes();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            if (alert.DidUserConfirm("cancel and revert any and all changes to default")) {
                if (data.SelectedRecipe != null) {
                    PopulateFields();
                } else {
                    CleanInputs();
                }
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

        private void AddRecipeWindow_Closing(object sender, EventArgs e) {
            if (RecipeName.Text != "") {
                if (alert.DidUserConfirm("close this window and return to the main window")) {
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

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e) {
            int id = 0;

            foreach (Recipe recipe in data.Recipes) {
                if (recipe.Name == RecipeName.Text) {
                    id = recipe.Id;
                    data.Recipes.Remove(recipe);
                    break;
                }    
            }
            
            if (id == 0) {
                id = data.Recipes.Count + 1;
            }

            if (alert.DidUserConfirm("save the recipe")) {
                Recipe recipe = new Recipe(
                    id,
                    RecipeName.Text,
                    Description.Text,
                    (Cuisine)CuisineSelect.SelectedValue,
                    types,
                    measurements,
                    instructions,
                    (Difficulty)DifficulySelect.SelectedValue
                );

                data.Recipes.Add(recipe);
                update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);
                CleanInputs();
                UpdateListBoxes();
            }
        }

        private void Instruction_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                if (ActiveInstructions.SelectedItem == null) {
                    instructions.Add(Instruction.Text);
                    Instruction.Text = string.Empty;
                } else {
                    int index = ActiveInstructions.SelectedIndex;
                    instructions.Remove(ActiveInstructions.SelectedItem.ToString());
                    instructions.Insert(index, Instruction.Text);
                }
                UpdateListBoxes();
            }
        }

        private void Type_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                types.Add(Type.Text);
                Type.Text = string.Empty;
                UpdateListBoxes();
            }
        }

        private void Ingredient_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                measurements.Add(Ingredient.Text, Amount.Text);
                Ingredient.Text = string.Empty;
                Amount.Text = string.Empty;
                UpdateListBoxes();
            }
        }

        private void RemoveSelectionFromList(List<string> targetList, ListBox targetListBox) {
            if (targetListBox.SelectedItem != null) {
                if (targetListBox != ActiveIngredients) {
                    targetList.Remove(targetListBox.SelectedItem.ToString());
                } else {
                    measurements.Remove(targetListBox.SelectedItem.ToString().Split(":")[0]);
                }
            }
        }

        private int MoveSelectionInList(bool isMovingUp, List<string> targetList, ListBox targetListBox) {
            int index = targetListBox.SelectedIndex;

            if (isMovingUp) {
                index -= 1;
            } else {
                index += 1;
            }

            if (index < 0) { index = 0; }
            if (index >= targetListBox.Items.Count) { index = targetListBox.Items.Count - 1; }

            if (targetListBox != ActiveIngredients) {
                if (targetListBox.SelectedItem != null) {
                    targetList.Remove(targetListBox.SelectedItem.ToString());
                    targetList.Insert(index, targetListBox.SelectedItem.ToString());
                }
            }

            return index;
        }

        private void InstructionsList_KeyDown(object sender, KeyEventArgs e) {
            int index = 0;

            if (e.Key == Key.Delete) {
                RemoveSelectionFromList(instructions, ActiveInstructions);
            } else if (e.Key == Key.Up) {
                index = MoveSelectionInList(true, instructions, ActiveInstructions);
            } else if (e.Key == Key.Down) {
                index = MoveSelectionInList(false, instructions, ActiveInstructions);
            }

            UpdateListBoxes();
            ActiveInstructions.SelectedIndex = index;
        }

        private void IngredientList_KeyDown(object sender, KeyEventArgs e) {
            int index = 0;

            if (e.Key == Key.Delete) {
                RemoveSelectionFromList(ingredients, ActiveIngredients);
            } else if (e.Key == Key.Up) {
                e.Handled = true;
                index = MoveSelectionInList(true, ingredients, ActiveIngredients);
            } else if (e.Key == Key.Down) {
                e.Handled = true;
                index = MoveSelectionInList(false, ingredients, ActiveIngredients);
            }

            UpdateListBoxes();
            ActiveIngredients.SelectedIndex = index;
        }

        private void TypeList_KeyDown(object sender, KeyEventArgs e) {
            int index = 0;

            if (e.Key == Key.Delete) {
                RemoveSelectionFromList(types, ActiveTypes);
            } else if (e.Key == Key.Up) {
                e.Handled = true;
                index = MoveSelectionInList(true, types, ActiveTypes);
            } else if (e.Key == Key.Down) {
                e.Handled = true;
                index = MoveSelectionInList(false, types, ActiveTypes);
            }

            UpdateListBoxes();
            ActiveTypes.SelectedIndex = index;
        }

        private void ActiveInstructions_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (ActiveInstructions.SelectedItem != null) {
                Instruction.Text = ActiveInstructions.SelectedItem.ToString();
            }
        }
    }
}
