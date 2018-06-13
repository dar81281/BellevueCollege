using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericSearch;

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
            RecipesXMLLocation = XMLFileFinder.GetXMLRecipesPath();
            IngredientsXMLLocation = XMLFileFinder.GetXMLIngredientsPaths();
            Database.SetInitializer<RecipesContext>(new RecipesContextInitializer());
            using (RecipesContext context = new RecipesContext())
            {
                Recipes = (from Recipe r in context.Recipes
                           select r).ToList();
                Ingredients = (from Ingredient i in context.Ingredients
                               select i).ToList();
            }
        }

        public string[] RecipeFields(Recipe r)
        {
            List<string> stringList = new List<string>();
            string[] strings;

            //Get the string from the recipe and add them to the array
            stringList.Add(r.Title);
            stringList.Add(r.RecipeType);
            stringList.Add(r.Directions);

            if (r.Yield != null)
            {
                stringList.Add(r.Yield);
            }
            if (r.Comment != null)
            {
                stringList.Add(r.Comment);
            }
            if (r.ServingSize != null)
            {
                stringList.Add(r.ServingSize);
            }
            //Get the ingredients
            List<Ingredient> ingredients = (from Ingredient i in Ingredients
                                            where i.RecipeID == r.RecipeID
                                            select i).ToList();
            if (ingredients.Count > 0)
            {
                foreach(Ingredient i in ingredients)
                {
                    string description = i.Description;
                    stringList.Add(description);
                }
            }

            strings = stringList.ToArray();
            return strings;
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
