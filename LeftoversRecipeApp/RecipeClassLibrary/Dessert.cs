using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    class Dessert : Recipe
    {
        public string type { get; set; }

        public override string ToString()
        {
            return type + "-" + base.Title;
        }
    }
}
