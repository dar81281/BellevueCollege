using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public static class RecipeBuilder
    {
        //Will take strings and builds a recipe

        public static Recipe BuildRecipe(string title, string directions, string recipeType, int recipeID, string yeild, string servingSize, string comment)
        {
            switch (recipeType)
            {
                case "Meal Item":
                    MealItem m = new MealItem
                    {
                        Title = title,
                        Directions = directions,
                        RecipeType = recipeType,
                        RecipeID = recipeID
                    };
                    if (yeild != null)
                    {
                        m.Yield = yeild;
                    }
                    if (servingSize != null)
                    {
                        m.ServingSize = servingSize;
                    }
                    if (comment != null)
                    {
                        m.Comment = comment;
                    }
                    return m;
                case "Dessert":
                    Dessert d = new Dessert
                    {
                        Title = title,
                        Directions = directions,
                        RecipeType = recipeType,
                        RecipeID = recipeID
                    };
                    if (yeild != null)
                    {
                        d.Yield = yeild;
                    }
                    if (servingSize != null)
                    {
                        d.ServingSize = servingSize;
                    }
                    if (comment != null)
                    {
                        d.Comment = comment;
                    }
                    return d;
                default:
                    throw new Exception("Attempted to create a recipe of a non-supported type: " + recipeType);
            };
        }

        

    }
}

