using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class RecipesContext : DbContext
    {
        public RecipesContext() : base("RecipesContext")
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
            RecipesXMLLocation = file;
            file = Directory.GetFiles(root, nameIng, SearchOption.AllDirectories).FirstOrDefault();
            IngredientsXMLLocation = file;
        }
    }
}
