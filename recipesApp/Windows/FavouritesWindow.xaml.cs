using recipesApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace recipesApp.Windows
{
    /// <summary>
    /// Interaction logic for FavouritesWindow.xaml
    /// </summary>
    public partial class FavouritesWindow : Window
    {
        List<Recipes> recipe = new List<Recipes>();
     
        public FavouritesWindow()
        {
            InitializeComponent();
            getFavourites();
            View.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// get file name
        /// </summary>
        /// <returns>@"..\..\favourites.dat"</returns>
        public string getFileName()
        {
            return @"..\..\favourites.dat";
        }

        /// <summary>
        /// save favourites to file
        /// </summary>
        public void saveFavourites()
        {

            // write one serialised object to a binary file
            Stream filestream = File.Open(getFileName(), FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(filestream, recipe);
            filestream.Close();
            // read it back
        }
        /// <summary>
        /// Get favourites from file
        /// </summary>
        public void getFavourites()
        {
            Stream filestream = File.Open(getFileName(), FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            recipe = ((List<Recipes>)bformatter.Deserialize(filestream));
            filestream.Close();
  
            datagrid.ItemsSource = recipe;
          
          
        }
  

        /// <summary>
        /// View details of recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Click(object sender, RoutedEventArgs e)
        {
            View.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Hidden;
            int i = datagrid.SelectedIndex;
           
            Name.Text = recipe[i].name;
            Prep_time.Text = "preparation time: " +  recipe[i].prep_time.ToString();
            Num_serves.Text = "Number of Serves: " + recipe[i].Num_serves.ToString();
            Method.AppendText(recipe[i].method);
            List<ingredients> ingredient = new List<ingredients>();
            ingredient = recipe[i].Ingredients;
            for (int j = 0; j < ingredient.Count; j++)
            {
                ingredients.Items.Add(ingredient[j].getIngredient() + " "  + ingredient[j].getAmount());
            }
        }


        /// <summary>
        /// Delete recipe from favourites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int i = datagrid.SelectedIndex;
            recipe.RemoveAt(i);
        }

        /// <summary>
        /// goes back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_click(object sender, RoutedEventArgs e)
        {
            saveFavourites();
            MainWindow m = new MainWindow();
            m.Show();
            Close();
        }

        /// <summary>
        /// hides the view grid
        /// and shows the main grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backViw_click(object sender, RoutedEventArgs e)
        {
             View.Visibility = Visibility.Hidden;
              Main.Visibility = Visibility.Visible;
           
        }

        /// <summary>
        /// search function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_textchanged(object sender, EventArgs e)
        {
            string searchitem = search.Text.ToString();
            var list = from b in recipe
                       where b.name.ToLower().Contains(searchitem)
                       select b;
            datagrid.ItemsSource = list;
        }
   
    }
}
