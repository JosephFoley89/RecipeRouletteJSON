﻿using System.Collections.Generic;

namespace RecipeRoulletteJSON.Model {
    class Recipe {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cuisine { get; set; }
        public List<string> Types { get; set; } 
        public Dictionary<string, string> Measurements { get; set; }
        public List<string> Directions { get; set; }

        public override string ToString() { return this.Name; }

        public Recipe(int id, string name, string description, string cuisine, List<string> types, Dictionary<string, string> measurements, List<string> directions) {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Cuisine = cuisine;
            this.Types = types;
            this.Measurements = measurements;
            this.Directions = directions;
        }
    }
}