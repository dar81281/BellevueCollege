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
using System.Windows.Shapes;

namespace LeftoversRecipeApp
{
    /// <summary>
    /// Interaction logic for AddRecipeDialog.xaml
    /// </summary>
    public partial class AddRecipeDialog : Window
    {
        public AddRecipeDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            errorLabel.Content = "";
            string[] acceptedRecipeTypes = { "Meal Item", "Dessert" };
            recipeTypeListBox.DataContext = acceptedRecipeTypes;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                errorLabel.Content = "Error: Recipe must have a title.";
            }
            if (String.IsNullOrWhiteSpace(directionTextBox.Text))
            {
                errorLabel.Content = "Error: Recipe must have directions.";
            }
            if (recipeTypeListBox.SelectedItem == null)
            {
                errorLabel.Content = "Error: Recipe must have an accepted Recipe Type.";
            }
            if (!String.IsNullOrWhiteSpace(titleTextBox.Text) && !String.IsNullOrWhiteSpace(directionTextBox.Text) && recipeTypeListBox.SelectedItem != null)
            {
                DialogResult = true;
            }
        }
    }
}
