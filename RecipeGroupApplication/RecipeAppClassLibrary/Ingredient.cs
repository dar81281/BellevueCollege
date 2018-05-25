using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecipeAppClassLibrary
{
    public class Ingredient : IComparable<Ingredient>
    {
        [Key]
        public string IngredientID { get; set; }
        public string RecipeID { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }

        int IComparable<Ingredient>.CompareTo(Ingredient other)
        {
            return Description.CompareTo(other.Description);
        }
    }
}
