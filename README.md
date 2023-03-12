# RecipeRouletteJSON

I set out to develop an application that randomly generates a meal plan based on familiar recipes. The initial plan was to utilize a database but I settled on JSON for ease of use.

Current Features:
  - The ability to select from two pre-existing recipes.
  - Once a recipe is selected, a description of the recipe, its required ingredients, and the instructions on how to create the dish are populated.
  - The ability to add and delete recipes
  - Email a recipe's instructions
  - Email a grocery list based on a randomly generated meal plan.

Planned Features:
  - The ability to edit recipes
  - The user will be able to select by filtering cuisines, food types (i.e. soups, cakes, pizzas etc.), difficulty, things you already have the ingredients for, etc. at a later date.
  - The ability to create an inventory of your current ingredients also housed in json
  - A better GUI
  - Who knows what scope creep has in store!

The application reads a config file that will need to be placed in a specific location: C:\RecipeRoulette\config.json. This is created in application if the file/directory doesn't exist. The config window will request SMTP information. I chose a free smtp server for my local machine. You can find more information about them here: https://www.sendinblue.com/free-smtp-server/ 
