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

namespace GenericSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] separator = new string[] { "," };

        public string[] SearchTerms
        {        
            get
            {
                string[] words = txtSearchWords.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                //for (int i = 0; i < words.Length; i++)
                //{
                //    words[i] = words[i].Trim();
                //    i++;
                //}
                List<string> list = new List<string>();
                foreach (string word in words)
                {
                    if (word != " ")
                    {
                        list.Add(word.Trim());
                    }
                }

                string[] words1 = list.ToArray();
                return words1;
            }       
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
