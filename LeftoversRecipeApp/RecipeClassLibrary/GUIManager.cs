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
            RefreshData();
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

        private Recipe CreateRecipe(Recipe r)
        {
            switch (r.RecipeType)
            {
                case "Meal Item":
                    MealItem m = new MealItem {
                        RecipeID = r.RecipeID,
                        RecipeType = r.RecipeType,
                        Title = r.Title,
                        Directions = r.Directions};
                    if (r.Comment != null)
                    {
                        m.Comment = r.Comment;
                    }
                    if (r.Yield != null)
                    {
                        m.Yield = r.Yield;
                    }
                    if (r.ServingSize != null)
                    {
                        m.ServingSize = r.ServingSize;
                    }
                    return m;
                case "Dessert":
                    Dessert d = new Dessert
                    {
                        RecipeID = r.RecipeID,
                        RecipeType = r.RecipeType,
                        ServingSize = r.ServingSize,
                        Title = r.Title,
                        Directions = r.Directions };
                    if (r.Comment != null)
                    {
                        d.Comment = r.Comment;
                    }
                    if (r.Yield != null)
                    {
                        d.Yield = r.Yield;
                    }
                    if (r.ServingSize != null)
                    {
                        d.ServingSize = r.ServingSize;
                    }
                    return d;
                default:
                    return r;
            }
        }

        public void RefreshData()
        {
            using (RecipesContext context = new RecipesContext())
            {
                List<Recipe> rawRecipes = (from Recipe r in context.Recipes
                                           select r).ToList();
                Recipes = new List<Recipe>();
                foreach (Recipe r in rawRecipes)
                {
                    Recipe newRecipe = CreateRecipe(r);
                    Recipes.Add(newRecipe);
                }
                Ingredients = (from Ingredient i in context.Ingredients
                               select i).ToList();
            }
        }

        public void AddNewRecipe(Recipe r)
        {
            try
            {
                using (RecipesContext context = new RecipesContext())
                {
                    context.Recipes.Add(r);
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {

                throw e;
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
