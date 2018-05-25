using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipeClassLibrary
{
    public class RecipesContextInitializer : DropCreateDatabaseIfModelChanges<RecipesContext>
    {
        protected override void Seed(RecipesContext context)
        {
            // Load the RecipesContext database with data XML files

            // Load Recipes from XML file
            List<Recipe> recipes = GetRecipeDataFromXDocument();

            foreach (Recipe recipe in recipes)
            {
                context.Recipes.Add(recipe);
            }

            context.SaveChanges();

            // Load ingredients from XML
            List<Ingredient> ingredients = GetIngredientDataFromXDocument();

            foreach (Ingredient ingredient in ingredients)
            {
                context.Ingredients.Add(ingredient);
            }

            context.SaveChanges();
        }
        //***************************

        /// <summary>
        /// Query loaded XDocument to retrieve recipes to a List.
        /// </summary>
        /// <returns></returns>
        static List<Recipe> GetRecipeDataFromXDocument()
        {
            //Get contents of XML file using LINQ to XML.
            var recipesXML = (
                from r in XDocument.Load("Recipes.xml").Descendants("Recipe")
                select r).ToList();

            // Set up collection to store contents from XML file
            List<Recipe> recipes = new List<Recipe>(recipesXML.Count);

            Recipe recipe = null;

            // Store contents from LINQ to XML into List collection
            foreach (var rec in recipesXML)
            {
                recipe = new Recipe();

                foreach(XElement x in rec.Elements())
                {
                    switch (x.Name.ToString())
                    {
                        case "RecipeID":
                            recipe.RecipeID = rec.Element("RecipeID").Value.Trim();
                            break;
                        case "Title":
                            recipe.Title = rec.Element("Title").Value.Trim();
                            break;
                        case "RecipeType":
                            recipe.RecipeType = rec.Element("RecipeType").Value.Trim();
                            break;
                        case "ServingSize":
                            recipe.ServingSize = rec.Element("ServingSize").Value.Trim();
                            break;
                        case "Yield":
                            recipe.Yield = rec.Element("Yield").Value.Trim();
                            break;
                        case "Directions":
                            recipe.Directions = rec.Element("Directions").Value.Trim();
                            break;
                        case "Comment":
                            recipe.Comment = rec.Element("Comment").Value.Trim();
                            break;
                    }
                }
                //Add to List collection
                recipes.Add(recipe);
            }

            return recipes;
        }

        /// <summary>
        /// Query loaded XDocument to place ingredient data into a List.
        /// </summary>
        /// <returns></returns>
        static List<Ingredient> GetIngredientDataFromXDocument()
        {
            //Get contents of XML file using LINQ to XML.
            var ingredientsXML = (
                from i in XDocument.Load("Ingredients.xml").Descendants("Ingredient")
                select i).ToList();

            // Set up collection to store contents from XML file
            List<Ingredient> ingredients = new List<Ingredient>(ingredientsXML.Count);

            Ingredient ingredient = null;

            // Store contents from LINQ to XML into List collection
            foreach (var i in ingredientsXML)
            {
                ingredient = new Ingredient();


                ingredient.IngredientID = i.Element("IngredientID").Value.Trim();
                ingredient.RecipeID = i.Element("RecipeID").Value.Trim();
                ingredient.Description = i.Element("Description").Value.Trim();

                //Appending the row to the List collection
                ingredients.Add(ingredient);
            }




            return ingredients;
        }
    }
}
