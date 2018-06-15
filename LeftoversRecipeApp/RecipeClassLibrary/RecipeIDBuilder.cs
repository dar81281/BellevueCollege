using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("RecipesTester")]
namespace RecipeClassLibrary
{
    public static class RecipeIDBuilder
    {
        public static int GetRecipeID(List<Recipe> unsortedList)
        {
            //int newID = 1;
            //if (unsortedList.Count == 0)
            //{
            //    return newID;
            //}
            //List<Recipe> recipes = (from Recipe r in unsortedList
            //                        orderby r.RecipeID
            //                        select r).ToList();

            //foreach (Recipe r in recipes)
            //{
            //    if (r.RecipeID > newID)
            //    {
            //        return newID;
            //    }
            //    newID++;
            //}
            //return newID;
            int newID = 1;
            if (unsortedList.Count == 0)
            {
                return newID;
            }

            while (true)
            {
                if (!unsortedList.Any(r => r.RecipeID == newID))
                {
                    return newID;
                }
                newID++;
            }
        }
    }
}
