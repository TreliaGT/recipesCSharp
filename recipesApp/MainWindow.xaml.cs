﻿using recipesApp.Classes;
using recipesApp.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        FavouritesWindow fw = new FavouritesWindow();
        List<ingredients> ingredients = new List<ingredients>();
        List<Recipes> recipe = new List<Recipes>();
        string ID; //get ingredient Id
        string RecipeId;

        public MainWindow()
        {
            InitializeComponent();
            View.Visibility = Visibility.Hidden;
            addI.Visibility = Visibility.Hidden;
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
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            getFavourites();
            getDataNewFavourite(drv, drv["id"].ToString());
        }

        /// <summary>
        /// Save favourites to a file
        /// </summary>
        public void saveFavourites()
        {
              // write one serialised object to a binary file
              Stream filestream = File.Open(fw.getFileName(), FileMode.Create);
              BinaryFormatter bformatter = new BinaryFormatter();
              bformatter.Serialize(filestream, recipe);
              filestream.Close();
            MessageBox.Show("have been added to favourites");
              // read it back
        }

        /// <summary>
        /// gets favourites from a file
        /// </summary>
        public void getFavourites()
        {
            Stream filestream = File.Open(fw.getFileName(), FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            recipe = ((List<Recipes>)bformatter.Deserialize(filestream));
            filestream.Close();
        }
       

        /// <summary>
        /// Adds a favourite to a file
        /// </summary>
        /// <param name="id"></param>
        public void getDataNewFavourite(DataRowView drv, string id)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(conn.getConnectionString());
            using (SqlCommand command = new SqlCommand("SELECT ingredients.ingredient, recipes_ingredents.Amount FROM recipes_ingredents, ingredients Where  ingredients.Id = recipes_ingredents.ingredent_id AND recipe_id = " + id))
            {

                command.Connection = cnn;
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    ingredients.Add(new ingredients(reader.GetString(0), reader.GetString(1)));
                }
            };
            recipe.Add(new Recipes(drv["detail"].ToString(),Convert.ToInt32(drv["preparation_time"]), drv["method"].ToString(), Convert.ToInt32(drv["num_serves"].ToString()), ingredients));
           saveFavourites();
        }

        /// <summary>
        /// get information for edit and view
        /// </summary>
        public void findDetails()
        {
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            ID = drv["id"].ToString();
            RecipeId = drv["id"].ToString();
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
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;

            cnn = new SqlConnection(conn.getConnectionString());

            string method = ConvertRichTextBoxContentsToString(ViewTxtMethod);
            sql = "UPDATE recipes SET detail='" + ViewTxtName.Text + "', preparation_time = '" + ViewTxtPrepTime.Text + "' , method = '" +  method + "', num_serves= '" + ViewTxtNumServes.Text + "' WHERE id =" + ID;
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
            connection();
        }


        //Edit ingredients section
        /// <summary>
        /// add Ingredients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddI_Click(object sender, RoutedEventArgs e)
        { 
            View.Visibility = Visibility.Hidden;
            addI.Visibility = Visibility.Visible;
            ingredientsfill();
        }

        /// <summary>
        /// fills ingredient table
        /// </summary>
        public void ingredientsfill()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ingredients", conn.getConnectionString());
            DataSet ds = new DataSet();
            da.Fill(ds, "ingredients");
            addIngredient.ItemsSource = ds.Tables["ingredients"].DefaultView;
        }

        /// <summary>
        /// allows to search through edit ingredients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchI_textchanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ingredients WHERE ingredient LIKE '%" + searchI.Text + "%'", conn.getConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "ingredents");
                addIngredient.ItemsSource = ds.Tables["ingredents"].DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("error has occured: " + x);
            }
        }

        ArrayList ingredentslist = new ArrayList();
        /// <summary>
        /// Checks ammount 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIR_Click(object sender, RoutedEventArgs e)
        {
            if (EditIAmount.Text == "")
            {
                MessageBox.Show("please add an amount in the textbox next to add amount");
            }
            else if (EditIAmount.Text == null)
            {
                MessageBox.Show("please add an amount in the textbox next to add amount");
            }
            else
            {
                DataRowView drv = (DataRowView)datagrid.SelectedItem;
                ID = drv["id"].ToString();
                string ingredient = ID + ", '" + EditIAmount.Text.ToString() + "'";
                ingredentslist.Add(ingredient);
            }
        }

        private void BacktoView_Click(object sender, RoutedEventArgs e)
        {
            View.Visibility = Visibility.Visible;
            addI.Visibility = Visibility.Hidden;
            editToJoinTable();
        }

        /// <summary>
        /// adds data into the recipes_ingredents table
        /// </summary>
        public void editToJoinTable()
        {
            SqlConnection cnn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;


            foreach (string item in ingredentslist) // Loop through List with foreach
            {
                cnn = new SqlConnection(conn.getConnectionString());


                sql = "insert into recipes_ingredents (recipe_id, ingredent_id, Amount) values(" + RecipeId + ", " + item + ")";

                try
                {
                    cnn.Open();
                    adapter.InsertCommand = new SqlCommand(sql, cnn);
                    adapter.InsertCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }



        //delete section
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
