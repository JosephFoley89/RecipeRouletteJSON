using RecipeRouletteJSON.Model;
using RecipeRouletteJSON.ProjectData;
using System.Collections.Generic;

namespace RecipeRoulletteJSON.Model {
    class Recipe {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Cuisine Cuisine { get; set; }
        public List<string> Course { get; set; } 
        public Dictionary<string, string> Measurements { get; set; }
        public List<string> Directions { get; set; }
        public Difficulty Difficulty { get; set; }

        public override string ToString() { return this.Name; }

        public Recipe(int id, string name, string description, Cuisine cuisine, List<string> course, Dictionary<string, string> measurements, List<string> directions, Difficulty difficulty) {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Cuisine = cuisine;
            this.Course = course;
            this.Measurements = measurements;
            this.Directions = directions;
            this.Difficulty = difficulty;
        }
    }
}
