using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RecipeAppClassLibrary;
using System.Data.Entity;
using System.Xml;
using System.Xml.Linq;

namespace RecipeGroupApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Database.SetInitializer<RecipesContext>(new Recip)
            using (RecipesContext db = new RecipesContext())
            {
                
                


                List<XElement> recipeReader = XDocument.Load("Recipes.xml")
                                     .Descendants("Recipe").ToList();
                List<XElement> ingredientReader = XDocument.Load("Ingredients.xml")
                                     .Descendants("Ingredient").ToList();

                //Code snippet from Wes
                //XDocument xmldoc = XDocument.Load(xmlStream);
                //var items = (from i in xmldoc.Descendants("item")
                //             select new { Item = i.Element("SEL").Value, Value = i.Element("VALUE").Value }).ToList();

                //listBox1.DataSource = items;
                //listBox1.DisplayMember = "Item";
                //listBox1.ValueMember = "Value";
            }
        }
        public Recipe[] GetRecipesFromXDocument()
        {
            List<XElement> recipeReader = XDocument.Load("Recipes.xml")
                                     .Descendants("Recipe").ToList();
            Recipe newRecipe;
            foreach(var rec in recipeReader)
            {
                newRecipe = new Recipe();
                newRecipe.RecipeID = recipeReader.Element("RecipeID").Value.Trim();
                newRecipe.RecipeType = recipeReader.Element("RecipeType").Value.Trim();
            }

            return recipeReader;
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
