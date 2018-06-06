using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class GUIManager: IDisposable
    {
        
        public static string RecipesXMLLocation { get; private set; }
        public static string IngredientsXMLLocation { get; private set; }
        public List<Recipe> Recipes { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }

        public GUIManager()
        {
            Database.SetInitializer<RecipesContext>(new RecipesContextInitializer());
            using (RecipesContext context = new RecipesContext())
            {
                Recipes = (from Recipe r in context.Recipes
                           select r).ToList();
                Ingredients = (from Ingredient i in context.Ingredients
                               select i).ToList();

                RecipesXMLLocation = XMLFileFinder.GetXMLRecipesPath();
                IngredientsXMLLocation = XMLFileFinder.GetXMLIngredientsPaths();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    using (RecipesContext context = new RecipesContext())
                    {
                        List<Recipe> recipes = (from Recipe r in context.Recipes
                                                select r).ToList();
                        XMLSerializer.XMLRecipeSerializer(recipes, RecipesXMLLocation);

                        List<Ingredient> ingredients = (from Ingredient i in context.Ingredients
                                                        select i).ToList();
                        XMLSerializer.XMLIngredientSerializer(ingredients, IngredientsXMLLocation);
                    }
                }

                Recipes = null;
                Ingredients = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion



    }
}
