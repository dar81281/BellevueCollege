using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecipeClassLibrary
{
    public static class XMLFileFinder
    {
        private const string RECIPESXML = "Recipes.xml";
        private const string INGREDIENTSXML = "Ingredients.xml";

        //Methods to find the wayward xml files
        public static string GetXMLRecipesPath(string nameRecipes = RECIPESXML)
        {
            string root;
            try
            {
                root = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
            catch
            {
                root = @"..\..\..\..\..\..\..\";
            }
            string file = Directory.GetFiles(root, nameRecipes, SearchOption.AllDirectories).FirstOrDefault();
            if (file == null)
            {
                file = RECIPESXML;
            }
            return file;
        }
        public static string GetXMLIngredientsPaths(string nameIngredient = INGREDIENTSXML)
        {
            string root;
            try
            {
                root = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
            catch
            {
                root = @"..\..\..\..\..\..\..\";
            }
            string file = Directory.GetFiles(root, nameIngredient, SearchOption.AllDirectories).FirstOrDefault();
            if (file == null)
            {
                file = INGREDIENTSXML;
            }
            return file;

        }
    }
}
