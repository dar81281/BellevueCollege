﻿using System;
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
                
                titleLabel.Content = "Please select a recipe from the list.";
                errorLabel.Content = "";
                yieldGUILabel.IsEnabled = false;
                yieldLabel.IsEnabled = false;
                yieldLabel.Content = "";
                servingSizeLabel.IsEnabled = false;
                servingSizeTextBox.IsEnabled = false;
                commentTextBox.IsEnabled = false;
                commentLabel.IsEnabled = false;
                recipeTypeLabel.IsEnabled = false;
                recipeTypeLabel.Content = "";
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
            //saving the recipes
            //XDocument document = new XDocument(
            //    new XDeclaration("1.0", "utf-8", "yes"),
            //        new XComment("Contents of Recipes table in database"),
            //        new XAttribute("Recipes",
            //            from r in context.Recipes
            //            select new XAttribute("Recipe",
            //                   new XElement("RecipeID", r.RecipeID),
            //                   new XElement("Title", r.Title),
            //                   new XElement("RecipeType", r.RecipeID),
            //                   r.ServingSize == null ? null :
            //                   new XElement("ServingSize", r.ServingSize),
            //                   r.Yield == null ? null :
            //                   new XElement("Yield", r.Yield),
            //                   new XElement("Directions", r.Directions),
            //                   r.Yield == null ? null :
            //                   new XElement("Comment", r.Comment))));
            //document.Save(context.RecipesXMLLocation);
            //document = new XDocument(
            //    new XDeclaration("1.0", "utf-8", "yes"),
            //        new XComment("Contents of the Ingredients table in databse"),
            //        new XElement("Ingredients",
            //            from i in context.Ingredients
            //            select new XElement("Ingredient",
            //                   new XElement("IngredientID", i.IngredientID),
            //                   new XElement("RecipeID", i.RecipeID),
            //                   new XElement("Description", i.Description))));
            //document.Save(context.IngredientsXMLLocation);
            //context.Dispose();

            RecipesContext dbContext = new RecipesContext();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.Indent = true;


            List<Recipe> recipes = (from r in dbContext.Recipes
                                    select r).ToList();

            List<Ingredient> ingredients = (from i in dbContext.Ingredients
                                            select i).ToList();

            var query1 = dbContext.Recipes.AsEnumerable<Recipe>();
            var query2 = dbContext.Ingredients.AsEnumerable<Ingredient>();

            //Create Recipe XML doc
            using (XmlWriter xw1 = XmlWriter.Create("Recipes.xml", xws))
            {
                XDocument Recipe = new XDocument(
                    new XElement("Recipes",
                      from r in query1
                      select new XElement("Recipe",
                        new XElement("RecipeID", r.RecipeID),
                          new XElement("Title", r.Title),
                          new XElement("RecipeType", r.RecipeType),
                          new XElement("ServingSize", r.ServingSize),
                          new XElement("Yield", r.Yield),
                          new XElement("Directions", r.Directions),
                          new XElement("Comment", r.Comment)
                          )
                      )
                    );

                Recipe.Save(xw1);
            }

            using (XmlWriter xw2 = XmlWriter.Create("Ingredients.xml", xws))
            {
                //Create Ingredients XML Doc
                XDocument Ingredients = new XDocument(
                    new XElement("Ingredients",
                      from i in query2
                      select new XElement("Ingredient",
                        new XElement("IngredientID", i.IngredientID),
                        new XElement("RecipeID", i.RecipeID),
                        new XElement("Description", i.Description)
                          )
                      )
                    );
                Ingredients.Save(xw2);
            }
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
