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
using System.Data.Entity;
using RecipeClassLibrary;
using System.Xml.Linq;
using System.Xml;

namespace LeftoversRecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GUIManager context;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Turn off unused controls, will be activated by selecting recipes
                context = new GUIManager();
                ClearFields();
                titleLabel.Content = "Please select a recipe from the list.";
                //Setup database and listbox
                Recipe[] recipes = (from r in context.Recipes
                                    orderby r.Title
                                    select r).ToArray();
                recipeListBox.DataContext = recipes;
            }
            catch ( Exception ex)
            {
                errorLabel.Content = ex.ToString();
                errorLabel.IsEnabled = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            context.Dispose();
          
        }

    private void recipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (recipeListBox.SelectedItem != null)
                {
                    Recipe recipe = (Recipe)recipeListBox.SelectedItem;
                    ClearFields();
                    titleLabel.IsEnabled = true;
                    titleLabel.Content = recipe.Title;
                    recipeTypeLabel.Content = recipe.RecipeType;
                    recipeTypeLabel.IsEnabled = true;
                    typeGUILabel.IsEnabled = true;
                    directionsLabel.IsEnabled = true;
                    directionsTextBox.Text = recipe.Directions;
                    directionsTextBox.IsEnabled = true;
                    if (recipe.Yield != null)
                    {
                        yieldGUILabel.IsEnabled = true;
                        yieldLabel.Content = recipe.Yield;
                        yieldLabel.IsEnabled = true;
                    }
                    if (recipe.ServingSize != null)
                    {
                        servingSizeLabel.IsEnabled = true;
                        servingSizeTextBox.IsEnabled = true;
                        servingSizeTextBox.Text = recipe.ServingSize;
                    }
                    if (recipe.Comment != null)
                    {
                        commentLabel.IsEnabled = true;
                        commentTextBox.Text = recipe.Comment;
                        commentTextBox.IsEnabled = true;
                    }
                    //Populate ingredients
                    ingredientsLabel.IsEnabled = true;
                    ingredientsListBox.IsEnabled = true;
                    ingredientsListBox.DataContext = (from i in context.Ingredients
                                                      where i.RecipeID == recipe.RecipeID
                                                      select i.Description).ToArray();
                }
                
            }
            catch (Exception ex)
            {
                errorLabel.Content = ex.ToString();
                errorLabel.IsEnabled = true;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            
            recipeListBox.SelectedItem = null;
            ClearFields();

        }
        private void ClearFields()
        {
            //Clear List and Text Boxes
            ingredientsListBox.DataContext = null;

            directionsTextBox.Clear();
            commentTextBox.Clear();
            servingSizeTextBox.Clear();

            //Gray out labels
            servingSizeLabel.IsEnabled = false;
            yieldGUILabel.IsEnabled = false;
            commentLabel.IsEnabled = false;

            //Clear Labels 

            titleLabel.Content = "";
            recipeTypeLabel.Content = "";
            yieldLabel.Content = "";
            errorLabel.Content = "";
        }
    }
}
