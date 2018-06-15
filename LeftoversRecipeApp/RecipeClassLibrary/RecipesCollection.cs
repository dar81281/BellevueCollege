using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class RecipesCollection: IEnumerable<Recipe>
    {
        private List<Recipe> Recipes = null;

        public RecipesCollection()
        {
            Recipes = new List<Recipe>();
        }

        public Recipe this[int indexInt]
        {
            get { return Recipes[indexInt]; }
            set { Recipes.Add(value); }
        }

        public void Sort()
        {
            Recipes.Sort();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        IEnumerator<Recipe> IEnumerable<Recipe>.GetEnumerator()
        {
            foreach (Recipe recipe in Recipes)
            {
                yield return recipe;
            }
        }
    }
}
