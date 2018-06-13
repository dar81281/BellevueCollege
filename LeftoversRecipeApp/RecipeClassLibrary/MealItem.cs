using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class MealItem : Recipe
    {

        public override string ToString()
        {
            return "MealItem - " + base.ToString();
        }
    }
}
