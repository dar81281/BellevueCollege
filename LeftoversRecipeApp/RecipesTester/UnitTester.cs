using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RecipeClassLibrary;
using LeftoversRecipeApp;

namespace RecipesTester
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void XMLRecipesDescendentsShouldMatchDatabaseRows()
        {
            RecipesContext context = new RecipesContext();
            int dbRecipesRows = context.Recipes.Count();
            int xmlRecipesDescendants = RecipesContextInitializer.GetRecipeDataFromXDocument(RecipesContext.GetXMLRecipesPath("Recipes.xml")).Count;

            Assert.AreEqual(dbRecipesRows, xmlRecipesDescendants);
        }
    }
}

