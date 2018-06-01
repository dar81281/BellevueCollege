using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using RecipeClassLibrary;
using LeftoversRecipeApp;
using System.Linq;

namespace RecipeTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void XMLDescendentsShouldMatchDatabaseRows()
        {
            RecipesContext context = new RecipesContext();
            int dbRecipesRows = context.Recipes.Count();

        }
    }
}
