# RecipeRoulletteJSON

I set out to develop an application that randomly generates a meal plan based on familiar recipes. The initial plan was to utilize a database but I settled on JSON.

Current Features:
  - The ability to select from two pre-existing recipes.
  - Once a recipe is selected, a description of the recipe will be displayed, its required ingredients, and the instructions on how to create the dish are populated.

Planned Features:
  - The ability to add, edit, and remove recipes.
  - The ability to randomly generate a meal plan.
      - The user will be able to select by filter cuisines, types - i.e. soups, cakes, etc. - and potentially other factors like flavor profile, difficulty, things you already have the ingredients for, etc. at a later date.
  - The ability to create an inventory of your current ingredients 
  - Who knows what scope creep has in store!

The application reads a config file that will need to be placed in a specific location: C:\RecipeRoulette\config.txt. You can specify where you want to place the recipe json file in the config file. There will likely be several json links inserted by the end of the project.
