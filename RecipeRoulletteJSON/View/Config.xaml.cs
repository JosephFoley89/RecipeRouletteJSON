using RecipeRouletteJSON.ProjectData;
using RecipeRouletteJSON.Utility;
using RecipeRoulletteJSON;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipeRouletteJSON.View {
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class Config : Window {
        private UpdateJSON update = new UpdateJSON();
        private Load load = new Load();
        private Data data;
        

        public Config() {
            InitializeComponent();
            if (Directory.Exists(@"C:\RecipeRoulette")) {
                data = load.LoadConfigFile();
                MultipleCheckBox.IsChecked = data.SaveMultipleBackups;
                Server.Text = data.Host;
                User.Text = data.Username;
                this.Key.Text = data.SMTPKey;
                Port.Text = data.Port.ToString();
                ToEmail.Text = data.ToEmail;
            }


            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            List<Recipe> recipes = new List<Recipe>();
            Data data = new Data();
            data.BackUpLocation = @"C:\RecipeRoulette\BackUps\";
            data.FileLocation = @"C:\RecipeRoulette\config.json";
            data.SaveMultipleBackups = (bool)MultipleCheckBox.IsChecked;
            data.Host = Server.Text;
            data.Username = User.Text;
            data.SMTPKey = Key.Text;
            data.ToEmail = ToEmail.Text;

            try {
                data.Port = Convert.ToInt16(Port.Text);
            } catch (Exception ex) {
                Alert alert = new Alert();
                alert.DisplayError("Please input a number in the Port textbox.", "Incorrect Value");
            }

            Recipe chili = new Recipe(
                1,
                "Chili",
                "This is an American adaptation of a Mexican classic. This is a recipe that my mother (she's the best!)"
                + "introduced me to but I have since \"improved\" the recipe to suit my tastes. Let's get to it!",
                Cuisine.American,
                new List<string> { "Main", "Soup" },
                new Dictionary<string, string> {
                    { "Crushed Tomatoes", "2 28oz Cans" },
                    { "Ground Sirloin", "2 pounds" },
                    { "Black Chili Beans", "2 15oz cans" },
                    { "Chili Pinto Beans", "2 15oz cans" },
                    { "Garbanzo Beans", "1 15oz can" },
                    { "White Onion", "1 Whole" },
                    { "Jalepeno Pepper", "3" },
                    { "Chili Powder", "1 Tablespoon" },
                    { "Ground Cumin", "1 Teaspoon" },
                    { "Cayenne Powder", "1/4 Tablespoon" },
                    { "Garlic Powder", "1/4 Tablespoon" },
                    { "Salt", "1 Teaspoon" },
                    { "Black Pepper", "1/4 Teaspoon" },
                    { "Brown Sugar", "To taste" },
                    { "Sriracha", "To taste" }
                },
                new List<string> {
                    "Combine the chili powder, ground cumin, cayenne powder, garlic powder, salt, and black pepper in a small bowl.",
                    "Mix the spices to combine and set to the side.",
                    "Chop the onion and jalepeno peppers.",
                    "Toss the onion and peppers into an oiled dutch oven and cook until translucent.",
                    "Place the ground sirloin into the same dutch oven and mix the onions and peppers into the beef.",
                    "Cook the beef until cooked through and caramelized.",
                    "Stir in the spice mixture and mix until well combined.",
                    "Stir in the crushed tomatoes.",
                    "Stir in the beans.",
                    "Stir in the brown sugar.",
                    "Stir in the sriracha.",
                    "Bring the mixture to a simmer and allow to cook for up to four hours stirring occasionally."
                },
                Difficulty.Easy
            );

            Recipe chickenSoup = new Recipe(
                2,
                "Southwestern Chicken Soup",
                "This is another one of the recipes my mother introduced me to. This recipe uses a unique ingredient, hominy, "
                + "which is delicious. Hominy is corn that has a potato like texture. It's delicious. Let's dig in!",
                Cuisine.Mexican,
                new List<string> { "Main", "Soup" },
                new Dictionary<string, string> {
                    { "Chicken", "1 Whole" },
                    { "Hominy", "1 25oz can" },
                    { "Diced Tomatoes and Green Chilis", "1 10oz can" },
                    { "Green Onions", "1 bunch" },
                    { "Cilantro", "1 bunch" },
                    { "Yellow Onion", "1" },
                    { "Celery", "4 stalks" },
                    { "Carrots", "4" },
                    { "Lime", "1" },
                    { "Water", "Unspecified" }
                },
                new List<string> {
                    "Roughly chop the carrots, yellow onion, and celery.",
                    "Place the chopped vegetables into a large pot.",
                    "Place the whole chicken - and any of those nasty bits, i.e. heart, neck, etc. - into the pot on top of the vegetables.",
                    "Take the pot over to the sink and slowly cover the chicken and vegetables in room temperature water.",
                    "Take the pot back over to the stove and bring to a boil.",
                    "Allow to cook until the chicken is cooked through. About 1.5 hours.",
                    "While the chicken is boiling, finely chop the cilantro and set to the side.",
                    "Slice the green onions into thin rings, ignoring the fibrous ends.",
                    "Once the chicken is fully cooked, remove the chicken from the newly formed broth and set to the side allowing it to cool.",
                    "Strain out all of the nasty chicken bits and used up vegetables and lower to a simmer.",
                    "Add the hominy, green chilis, green onions, and cilantro to the broth.",
                    "Pull apart the chicken, removing any of the bones and grody bits.",
                    "Roughly chop the chicken so that there are chunks of chicken.",
                    "Reintroduce the chicken into the broth.",
                    "Allow to simmer together for a few minutes."
                },
                Difficulty.Easy
            );

            recipes.Add(chili);
            recipes.Add(chickenSoup);
            data.Recipes = recipes;

            update.SaveRecipes(data, data.FileLocation, data.BackUpLocation, data.SaveMultipleBackups);

            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void ConfigWindow_Closing(object sender, EventArgs e) {
            this.Hide();
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }
    }
}
