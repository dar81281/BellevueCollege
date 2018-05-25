using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class Recipe : IComparable<Recipe>
    {
        public string RecipeID { get; set; }
        public string Title { get; set; }
        public string RecipeType { get; set; }
        public string ServingSize { get; set; }
        public string Yield { get; set; }
        public string Directions { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public int CompareTo(Recipe other)
        {
            return Title.CompareTo(other.Title);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
