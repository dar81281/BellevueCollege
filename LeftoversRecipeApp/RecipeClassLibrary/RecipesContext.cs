using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("RecipesTester")]
namespace RecipeClassLibrary
{
    
    public class RecipesContext : DbContext
    {
        public RecipesContext() : base("name=RecipesDB")
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        
    }
}
