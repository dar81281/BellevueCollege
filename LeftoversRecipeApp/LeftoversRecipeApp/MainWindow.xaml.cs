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
using GenericSearch;



namespace LeftoversRecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool exitButtonClicked = false;
        private string[] searchTerms;
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
                Recipe[] recipes = getRecipes();
                recipeListBox.DataContext = recipes;
            }
            catch (AccessViolationException ex)
            {
                var baseexception = ex.GetBaseException();
                errorLabel.Content = "Attempted to search a protected file: " + baseexception.Message;
            }
            catch ( Exception ex)
            {
                var baseexception = ex.GetBaseException();
                errorLabel.Content = baseexception.Message;
            }
        }
        private Recipe[] getRecipes()
        {
            Recipe[] recipes = (from r in context.Recipes
                                orderby r.RecipeType descending, r.Title
                                select r).ToArray();
            return recipes;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (context != null)
            {
                context.Dispose();
            }
          
        }

    private void recipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (recipeListBox.SelectedItem != null)
                {
                    Recipe recipe = (Recipe)recipeListBox.SelectedItem;
                    ClearFields();
                    titleLabel.Content = recipe.Title;
                    recipeTypeLabel.Content = recipe.RecipeType;
                    directionsTextBox.Text = recipe.Directions;
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
                    ingredientsListBox.DataContext = (from i in context.Ingredients
                                                      where i.RecipeID == recipe.RecipeID
                                                      select i.Description).ToArray();
                }
                
            }
            catch (Exception ex)
            {
                
                var baseexception = ex.GetBaseException();
                errorLabel.Content = baseexception.Message;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            exitButtonClicked = true;
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (exitButtonClicked)
            {
                base.OnClosing(e);
            }
            else
            {
                errorLabel.Content = "Please use the exit button to close the application";
                e.Cancel = true;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Remove Selection in the recipe listbox
            recipeListBox.SelectedItem = null;

            //Refresh the data from the database
            context.RefreshData();

            //Call getRecipes method and reload the recipe listbox
            Recipe[] recipes = getRecipes();
            recipeListBox.DataContext = recipes;

            //Call ClearFields method clear all other contents
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenericSearch.MainWindow dialog = new GenericSearch.MainWindow();
                if (dialog.ShowDialog() == true)
                {
                    searchTerms = dialog.SearchTerms;
                    ClearFields();
                    //Take a list of recipes, and for each create an array of string from its fields (and ingredients), then search it using the searchTerms,
                    //if the result is true than add it to a List and set the recipes listbox datacontext to the new list.
                    List<Recipe> foundRecipes = new List<Recipe>();
                    foreach (Recipe r in context.Recipes)
                    {
                        string[] recipeFields = context.RecipeFields(r);
                        if (StringSearcher.StringArrayAndSearch(recipeFields, searchTerms))
                        {
                            foundRecipes.Add(r);
                        }
                    }

                    //If no recipes were found then inform user in the error lable.
                    if (foundRecipes.Count > 0)
                    {
                        recipeListBox.SelectedItem = null;
                        foundRecipes.Sort();
                        recipeListBox.DataContext = foundRecipes;
                    }
                    else
                    {
                        recipeListBox.DataContext = foundRecipes;
                        errorLabel.Content = "No recipes were found matching your requirements.";
                    }
                }
            }
            catch (Exception ex)
            {

                var baseexception = ex.GetBaseException();
                errorLabel.Content = baseexception.Message;
            }
        }
    }
}
