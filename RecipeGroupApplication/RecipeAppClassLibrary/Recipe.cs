using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecipeAppClassLibrary
{
    public class Recipe: IComparable<Recipe>
    {
        [Key]
        public string RecipeID { get; set; }
        public string Title { get; set; }
        public string RecipeType { get; set; }
        public string ServingSize { get; set; }
        public string Yield { get; set;}
        public string Directions { get; set; }
        public string Comment { get; set; }



        //Override ToString
        public override string ToString()
        {
            return Title;
        }

        int IComparable<Recipe>.CompareTo(Recipe other)
        {
            return Title.CompareTo(other.Title);
        }
    }
}
