using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace recipesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBConnection conn = new DBConnection();

        public MainWindow()
        {
            InitializeComponent();
            connection();
        }
        /// <summary>
        /// connection and show data in datagrid
        /// </summary>
        public void connection()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Recipes", conn.getConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "recipes");
                datagrid.ItemsSource = ds.Tables["recipes"].DefaultView;

            }
            catch (Exception e)
            {
                MessageBox.Show("error has occured: " + e );
            }
        }
        /// <summary>
        /// search function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_textchanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM recipes WHERE detail LIKE '%" + Search.Text + "%'", conn.getConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "recipes");
                datagrid.ItemsSource = ds.Tables["recipes"].DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("error has occured: " + x);
            }
        }
        /// <summary>
        /// show add recipe menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddR_Click(object sender, RoutedEventArgs e)
        {
            addRecipe r = new addRecipe();
            r.Show();
        }

        /// <summary>
        /// shows favourites window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Favourite_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// add recipe to favourites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddF_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// view details click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// delete recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
