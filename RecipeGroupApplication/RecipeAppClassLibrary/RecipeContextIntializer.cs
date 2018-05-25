using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppClassLibrary
{
    public class RecipeContextIntializer: DropCreateDatabaseIfModelChanges<RecipesContext>
    {
        protected override void Seed(RecipesContext context)
        {
            base.Seed(context);
        }
    }
}
