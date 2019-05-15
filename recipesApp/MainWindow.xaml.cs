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
        string ID; //get ingredient Id
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
            Close r = new Close();
            r.Show();
            this.Close();
        }

        /// <summary>
        /// shows favourites window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Favourite_Click(object sender, RoutedEventArgs e)
        {
            //Favourites Window
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
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;

            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            ID = drv["id"].ToString();

            cnn = new SqlConnection(conn.getConnectionString());


            sql = "DELETE recipes WHERE id =" + ID;

            try
            {
                cnn.Open();
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Row Deleted !! ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection();
        }
    }
}
