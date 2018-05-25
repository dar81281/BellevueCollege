using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeGroupApplication
{
    public static class StartupClass
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            RecipeGroupApplication.App app = new RecipeGroupApplication.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
