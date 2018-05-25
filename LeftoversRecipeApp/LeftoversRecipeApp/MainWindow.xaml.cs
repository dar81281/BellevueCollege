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

namespace LeftoversRecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecipesContext context = new RecipesContext();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Turn off unused controls, will be activated by selecting recipes
                titleLabel.IsEnabled = false;
                errorLabel.Content = "";
                yieldGUILabel.IsEnabled = false;
                yieldLabel.IsEnabled = false;
                servingSizeLabel.IsEnabled = false;
                servingSizeTextBox.IsEnabled = false;
                commentTextBox.IsEnabled = false;
                commentLabel.IsEnabled = false;
                recipeTypeLabel.IsEnabled = false;
                typeGUILabel.IsEnabled = false;
                directionsLabel.IsEnabled = false;
                directionsTextBox.IsEnabled = false;
                ingredientsLabel.IsEnabled = false;
                ingredientsListBox.IsEnabled = false;
                //Setup database and listbox
                Database.SetInitializer<RecipesContext>(new RecipesContextInitializer());
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
                Recipe recipe = (Recipe)recipeListBox.SelectedItem;
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
                else
                {
                    yieldGUILabel.IsEnabled = false;
                    yieldLabel.Content = "";
                }
                if (recipe.ServingSize != null)
                {
                    servingSizeLabel.IsEnabled = true;
                    servingSizeTextBox.IsEnabled = true;
                    servingSizeTextBox.Text = recipe.ServingSize;
                }
                else
                {
                    servingSizeLabel.IsEnabled = false;
                    servingSizeTextBox.Text = "";
                }
                if (recipe.Comment != null)
                {
                    commentLabel.IsEnabled = true;
                    commentTextBox.Text = recipe.Comment;
                    commentTextBox.IsEnabled = true;
                }
                else
                {
                    commentLabel.IsEnabled = false;
                    commentTextBox.Text = "";
                }
                //Populate ingredients
                ingredientsLabel.IsEnabled = true;
                ingredientsListBox.IsEnabled = true;
                ingredientsListBox.DataContext = (from i in context.Ingredients
                                                  where i.RecipeID == recipe.RecipeID
                                                  select i.Description).ToArray();
                
            }
            catch (Exception ex)
            {
                errorLabel.Content = ex.ToString();
                errorLabel.IsEnabled = true;
            }
        }
    }
}
