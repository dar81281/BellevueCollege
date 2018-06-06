using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipeClassLibrary
{
    public static class XMLSerializer
    {
        public static void XMLRecipeSerializer(List<Recipe> Recipes, string RecipesXMLLocation)
        {
            //New lists are needed here due to LINQ to Entities requiring them.
            List<Recipe> recipes = (from r in Recipes
                                    orderby r.RecipeID
                                    select r).ToList();

            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Contents of Recipes table in database"),
                    new XElement("Recipes",
                        from r in recipes
                        select new XElement("Recipe",
                               new XElement("RecipeID", r.RecipeID),
                               new XElement("Title", r.Title),
                               new XElement("RecipeType", r.RecipeType),
                               r.ServingSize == null ? null :
                               new XElement("ServingSize", r.ServingSize),
                               r.Yield == null ? null :
                               new XElement("Yield", r.Yield),
                               new XElement("Directions", r.Directions),
                               r.Yield == null ? null :
                               new XElement("Comment", r.Comment))));
            // Check to make sure all recipes made it to the xml (should be 21 as of 31May)
            if (document.Descendants("Recipe").Count() == recipes.Count)
            {
                document.Save(RecipesXMLLocation);
            }

        }
        public static void XMLIngredientSerializer(List<Ingredient> Ingredients, string IngredientsXMLLocation)
        {
            //New lists are needed here due to LINQ to Entities requiring them.

            List<Ingredient> ingredients = (from i in Ingredients
                                            orderby i.IngredientID
                                            select i).ToList();
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Contents of the Ingredients table in databse"),
                    new XElement("Ingredients",
                        from i in ingredients
                        select new XElement("Ingredient",
                               new XElement("IngredientID", i.IngredientID),
                               new XElement("RecipeID", i.RecipeID),
                               new XElement("Description", i.Description))));
            // Check to ensure all ingredients made it to the xml (should be 170 as of 31May)
            if (document.Descendants("Ingredient").Count() == ingredients.Count)
            {
                document.Save(IngredientsXMLLocation);
            }
        }
    }
}
