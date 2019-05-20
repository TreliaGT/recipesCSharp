using recipesApp.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
            View.Visibility = Visibility.Hidden;
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
            FavouritesWindow f = new FavouritesWindow();
             f.Show();
            this.Close();
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
        /// get information for edit and view
        /// </summary>
        public void findDetails()
        {
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            ID = drv["id"].ToString();
            ViewTxtName.Text = drv["detail"].ToString();
            ViewTxtNumServes.Text = drv["num_serves"].ToString();
            ViewTxtPrepTime.Text = drv["preparation_time"].ToString();
            ViewTxtMethod.AppendText(drv["method"].ToString());
            getIngredients();
        }
        /// <summary>
        /// view details click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Click(object sender, RoutedEventArgs e)
        {
           
            ViewTxtName.IsReadOnly = true;
            ViewTxtNumServes.IsReadOnly = true;
            ViewTxtPrepTime.IsReadOnly = true;
            ViewTxtMethod.IsReadOnly = true;
            Main.Visibility = Visibility.Hidden;
            View.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
            btnAddI.Visibility = Visibility.Hidden;
            Ingredientslist.IsReadOnly = true;
            findDetails();
        }

        /// <summary>
        /// Get ingredient for recipe
        /// </summary>
        public void getIngredients()
        {
            try
             {
                 //   SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM recipes_ingredents WHERE recipe_id = " + ID + " INNER JOIN ingredients ON recipes_ingredents.ingredent_id = ingredients.ingredient ", conn.getConnectionString());
                 SqlDataAdapter da = new SqlDataAdapter("SELECT recipes_ingredents.Id, recipes_ingredents.recipe_id, recipes_ingredents.ingredent_id, recipes_ingredents.Amount, ingredients.ingredient,  ingredients.Id FROM recipes_ingredents, ingredients Where  ingredients.Id = recipes_ingredents.ingredent_id AND recipe_id = " + ID, conn.getConnectionString());
                 DataSet ds = new DataSet();
          
                 da.Fill(ds, "recipes_ingredents");
              
                 Ingredientslist.ItemsSource = (ds.Tables["recipes_ingredents"].DefaultView);
             }
             catch (Exception x)
             {
                 MessageBox.Show("error has occured: " + x);
             }

        }

        /// <summary>
        /// Close view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseView_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            View.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// edit view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditView_Click(object sender, RoutedEventArgs e)
        {
            findDetails();
            Main.Visibility = Visibility.Hidden;
            View.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Visible;
            btnAddI.Visibility = Visibility.Visible;
            Ingredientslist.IsReadOnly = false;
            ViewTxtName.IsReadOnly = false;
            ViewTxtNumServes.IsReadOnly = false;
            ViewTxtPrepTime.IsReadOnly = false;
            ViewTxtMethod.IsReadOnly = false;
        }

        /// <summary>
        /// edit recipes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
           


        }

        /// <summary>
        /// add Ingredients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddI_Click(object sender, RoutedEventArgs e)
        {



        }


        /// <summary>
        /// Delete Ingredient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteI_Click(object sender, RoutedEventArgs e)
        {
            if (Ingredientslist.IsReadOnly == false)
            {


                SqlConnection cnn;
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = null;
                string IDs;
                DataRowView drv = (DataRowView)Ingredientslist.SelectedItem;
                IDs = drv["Id"].ToString();

                cnn = new SqlConnection(conn.getConnectionString());


                sql = "DELETE recipes_ingredents WHERE Id = " + IDs;

                try
                {
                    cnn.Open();
                    adapter.InsertCommand = new SqlCommand(sql, cnn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Row Deleted ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                getIngredients();
            }
            else
            {
                MessageBox.Show("Must go back and press edit to be able to delete these");
            }
        }

        string ConvertRichTextBoxContentsToString(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
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


            sql = "DELETE recipes WHERE id =" + ID + ";" + "DELETE recipes_ingredents WHERE recipe_id = " + ID + ";";

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
