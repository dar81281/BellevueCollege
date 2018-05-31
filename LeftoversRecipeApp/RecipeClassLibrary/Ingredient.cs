using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecipeClassLibrary
{
    public class Ingredient : IComparable<Ingredient>
    {
        [Key]
        public int IngredientID { get; set; }
        [Required]
        public int RecipeID { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int CompareTo(Ingredient other)
        {
            return IngredientID.CompareTo(other.IngredientID);
        }
    }
}
