using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecipeClassLibrary
{
    public class Recipe : IComparable<Recipe>
    {
        [Key, Required]
        public int RecipeID { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(30)]
        public string RecipeType { get; set; }

        public string ServingSize { get; set; }

        public string Yield { get; set; }
        [Required]
        public string Directions { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public int CompareTo(Recipe other)
        {
            int compare = this.RecipeType.CompareTo(other.RecipeType);
            if (compare == 1)
            {
                return -1;
            }
            if (compare == -1)
            {
                return 1;
            }
            if (compare == 0)
            {
                compare = this.Title.CompareTo(other.Title);
            }
            return compare;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
