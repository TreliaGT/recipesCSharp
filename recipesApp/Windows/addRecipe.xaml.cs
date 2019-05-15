using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace recipesApp
{
    /// <summary>
    /// Interaction logic for addRecipe.xaml
    /// </summary>
    public partial class Close : Window
    {
        
        DBConnection conn = new DBConnection();
        string ID; //get ingredient Id
        public Close()
        {
            InitializeComponent();
            Connection();
        }

        // Ingredients section

        /// <summary>
        /// showing the ingredients in datagrid
        /// </summary>
        public void Connection()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ingredients", conn.getConnectionString());
            DataSet ds = new DataSet();
            da.Fill(ds, "ingredients");
            datagrid.ItemsSource = ds.Tables["ingredients"].DefaultView;      
        }
        /// <summary>
        /// Add ingredent to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddI_Click(object sender, RoutedEventArgs e)
        {
  
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;

            cnn = new SqlConnection(conn.getConnectionString());
           

            sql = "insert into ingredients (ingredient) values('"+ addI.Text + "')";

            try
            {
                cnn.Open();
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Row inserted !! ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Connection();
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ingredients WHERE ingredient LIKE '%" + search.Text + "%'", conn.getConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "ingredents");
                datagrid.ItemsSource = ds.Tables["ingredents"].DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("error has occured: " + x);
            }
        }


        /// <summary>
        /// view details click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddOrEdit.Click -= AddI_Click; // remove 
            AddOrEdit.Click += Editi_click; // add   
            AddOrEdit.Content = "Update Ingredient";
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            ID = drv["id"].ToString();
            addI.Text = drv["ingredient"].ToString();
        }

        public void EditIngredient()
        {
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;

            cnn = new SqlConnection(conn.getConnectionString());


            sql = "UPDATE ingredients SET ingredient='" + addI.Text + "' WHERE id =" + ID;

            try
            {
                cnn.Open();
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Row updated !! ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Connection();
            AddOrEdit.Click -= Editi_click; // remove 
            AddOrEdit.Click += AddI_Click; // add  
            AddOrEdit.Content = "Add ingredient";
        }

        /// <summary>
        /// view details click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editi_click(object sender, RoutedEventArgs e)
        {
            EditIngredient();
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


            sql = "DELETE ingredients WHERE id =" + ID;

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
            Connection();
        }


        // Adding to recipe section
        public string Rname;
        ArrayList ingredentslist = new ArrayList();
        /// <summary>
        /// add recipe to favourites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIR_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            ID = drv["id"].ToString();
            string ingredient = ID + ',' + TxtAmount.Text;
            ingredentslist.Add(ID);
     

        }

        private void AddRecipes(object sender, RoutedEventArgs e)
        {
            AddRecipesMethod();
        }

        private void AddRecipesMethod()
        {
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;

            cnn = new SqlConnection(conn.getConnectionString());
             Rname = txtRName.Text;
            string method = ConvertRichTextBoxContentsToString(Method);

            sql = "insert into recipes (detail, preparation_time, method, num_serves) values('" + Rname + "'," + Prep_Time.Text + ",'" + method + "'," + Num_Serves.Text + ')';

            try
            {
                cnn.Open();
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Recipe added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            addToJoinTable();
        }

        string ConvertRichTextBoxContentsToString(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        public void addToJoinTable()
        {
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;
            foreach (object item in ingredentslist) // Loop through List with foreach
            {
                    cnn = new SqlConnection(conn.getConnectionString());


                    sql = "insert into recipes_ingredents (recipe_id, ingredent_id, Amount) values(SELECT id FROM recipes WHERE detail =" + Rname + "," + item + ')';

                    try
                    {
                        cnn.Open();
                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Recipe added");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow r = new MainWindow();
            r.Show();
            this.Close();
        }
    }
}
