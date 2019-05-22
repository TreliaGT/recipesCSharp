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

namespace recipesApp.Windows
{
    /// <summary>
    /// Interaction logic for FavouritesWindow.xaml
    /// </summary>
    public partial class FavouritesWindow : Window
    {
        public FavouritesWindow()
        {
            InitializeComponent();
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
            /*  
              Employee emp = new Employee(1, "Jane Smith");
              // write one serialised object to a binary file
              Stream filestream = File.Open(getFileName(), FileMode.Create);
              BinaryFormatter bformatter = new BinaryFormatter();
              bformatter.Serialize(filestream, emp);
              filestream.Close();
              Console.WriteLine("Written the employee data tothe file");
          
              emp = null;*/
        }

        /// <summary>
        /// Get favourites from file
        /// </summary>
        public void getFavourites()
        {
            /*  filestream = File.Open(getFileName(), FileMode.Open);
              bformatter = new BinaryFormatter();
              emp =(Employee)bformatter.Deserialize(filestream);
              filestream.Close();
              Console.WriteLine("Employee Id: {0}",
             emp.empId.ToString());
              Console.WriteLine("Employee Name: {0}",
             emp.empName);
              Console.ReadLine();*/
        }

        /// <summary>
        /// View details of recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Delete recipe from favourites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            saveFavourites();
            MainWindow m = new MainWindow();
            m.Show();
            Close();
        }
    }
}
