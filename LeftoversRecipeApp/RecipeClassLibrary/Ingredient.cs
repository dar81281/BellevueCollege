using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class Ingredient
    {
        public string IngredientID { get; set; }
        public string RecipeID { get; set; }
        public string Description { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
