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
            using (RecipesContext db = new RecipesContext())
            {
                XmlReader recipeReader;
                XmlReader ingredientReader;

                //string connectionString;
                //connectionString = "Data Source=servername;Initial Catalog=databsename;User ID=username;Password=password";

                recipeReader = XmlReader.Create("Recipes.xml");
                ingredientReader = XmlReader.Create("Ingredients.xml");



                //Code snippet from Wes
                //XDocument xmldoc = XDocument.Load(xmlStream);
                //var items = (from i in xmldoc.Descendants("item")
                //             select new { Item = i.Element("SEL").Value, Value = i.Element("VALUE").Value }).ToList();

                //listBox1.DataSource = items;
                //listBox1.DisplayMember = "Item";
                //listBox1.ValueMember = "Value";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
