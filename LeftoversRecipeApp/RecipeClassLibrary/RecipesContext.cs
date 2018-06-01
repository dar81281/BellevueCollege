using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RecipeClassLibrary
{
    public class RecipesContext : DbContext
    {
        public RecipesContext() : base("name=RecipesDB")
        {
            GetXMLPaths(RECIPESXML, INGREDIENTSXML);
        }

        private const string RECIPESXML = "Recipes.xml";
        private const string INGREDIENTSXML = "Ingredients.xml";
        public string RecipesXMLLocation { get; private set; }
        public string IngredientsXMLLocation { get; private set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        //Methods to find the wayward xml files
        public void GetXMLPaths(string nameRec, string nameIng)
        {
            string root = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
            string file = Directory.GetFiles(root, nameRec, SearchOption.AllDirectories).FirstOrDefault();
            if (file == null)
            {
                file = RECIPESXML;
            }
            RecipesXMLLocation = file;
            file = Directory.GetFiles(root, nameIng, SearchOption.AllDirectories).FirstOrDefault();
            if (file == null)
            {
                file = INGREDIENTSXML;
            }
            IngredientsXMLLocation = file;
        }
        public void XMLSerializer()
        {
            //New lists are needed here due to LINQ to Entities requiring them.
            List<Recipe> recipes = (from r in Recipes
                                    orderby r.RecipeID
                                    select r).ToList();
            List<Ingredient> ingredients = (from i in Ingredients
                                            orderby i.IngredientID
                                            select i).ToList();
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Contents of Recipes table in database"),
                    new XElement("Recipes",
                        from r in recipes
                        select new XElement("Recipe",
                               new XElement("RecipeID", r.RecipeID),
                               new XElement("Title", r.Title),
                               new XElement("RecipeType", r.RecipeID),
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
            document = new XDocument(
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
        #region IDisposable Methods
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    XMLSerializer();
                }
                
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //Destructor will create the xml files from the Ingredients and Recipes properties and will save the files at the locations in the XMLLocation strings
        ~RecipesContext()
        {
            Dispose(false);
        }
        #endregion
    }
}
