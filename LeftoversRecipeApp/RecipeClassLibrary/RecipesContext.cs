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
        public RecipesContext() : base("name=RecipesDBConnectionString")
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
            try
            {
                string file = Directory.GetFiles(root, nameRec, SearchOption.AllDirectories).FirstOrDefault();
                RecipesXMLLocation = file;
                file = Directory.GetFiles(root, nameIng, SearchOption.AllDirectories).FirstOrDefault();
                IngredientsXMLLocation = file;
            }
            catch (System.UnauthorizedAccessException uae)
            {
                //Ignore UAE exceptions for now
            }
        }
        public void XMLSerializer()
        {

            RecipesContext dbContext = new RecipesContext();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.NewLineOnAttributes = true;
            xws.Indent = true;


            List<Recipe> recipes = (from r in dbContext.Recipes
                                    select r).ToList();

            List<Ingredient> ingredients = (from i in dbContext.Ingredients
                                            select i).ToList();

            var query1 = dbContext.Recipes.AsEnumerable<Recipe>();
            var query2 = dbContext.Ingredients.AsEnumerable<Ingredient>();

            //Create Recipe XML doc
            using (XmlWriter xw1 = XmlWriter.Create("Recipes.xml", xws))
            {
                XDocument Recipe = new XDocument(
                    new XElement("Recipes",
                      from r in query1
                      select new XElement("Recipe",
                        new XElement("RecipeID", r.RecipeID),
                          new XElement("Title", r.Title),
                          new XElement("RecipeType", r.RecipeType),
                          new XElement("ServingSize", r.ServingSize),
                          new XElement("Yield", r.Yield),
                          new XElement("Directions", r.Directions),
                          new XElement("Comment", r.Comment)
                          )
                      )
                    );

                Recipe.Save(xw1);

            }

            using (XmlWriter xw2 = XmlWriter.Create("Ingredients.xml", xws))
            {
                //Create Ingredients XML Doc
                XDocument Ingredients = new XDocument(
                    new XElement("Ingredients",
                      from i in query2
                      select new XElement("Ingredient",
                        new XElement("IngredientID", i.IngredientID),
                        new XElement("RecipeID", i.RecipeID),
                        new XElement("Description", i.Description)
                          )
                      )
                    );
                Ingredients.Save(xw2);
            }
        }

        //Destructor will create the xml files from the Ingredients and Recipes properties and will save the files at the locations in the XMLLocation strings
        ~RecipesContext()
        {

        }
    }
}
