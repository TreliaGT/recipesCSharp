using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using recipesApp;
using recipesApp.Classes;
using recipesApp.Windows;

namespace UnitTestRecipeApp
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// testing the connection string
        /// </summary>
        [TestMethod]
        public void TestConnString()
        {
            DBConnection conn = new DBConnection();
            string actualString = conn.getConnectionString();
            string expectedString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\V244682\Documents\recipesCSharp\recipesApp\recipesDB.mdf;Integrated Security=True";
            Assert.AreEqual(expectedString, actualString);

        }

        List<ingredients> i = new List<ingredients>();
        /// <summary>
        /// Testing the ingredient class
        /// </summary>
        [TestMethod]
        public void TestIngredient(){
         
            i.Add(new ingredients ("egg" , "1"));
            i.Add(new ingredients("Bacon", "2"));
            string expectedString = "Bacon";
            string expectedString2 = "egg";
            Assert.AreEqual(expectedString, i[1].getIngredient());
            Assert.AreEqual(expectedString2, i[0].getIngredient());
        }
        /// <summary>
        /// Testing the ingredient amount
        /// </summary>
        [TestMethod]
        public void TestIngredientAmount()
        {
            i.Add(new ingredients("egg", "1"));
            i.Add(new ingredients("Bacon", "2"));
            string expectedString = "1";
            string expectedString2 = "2";
            Assert.AreEqual(expectedString, i[0].getAmount());
            Assert.AreEqual(expectedString2, i[1].getAmount());
        }

        /// <summary>
        /// Testing Recipes Class
        /// </summary>
        [TestMethod]
        public void TestingRecipes()
        {
            List<Recipes> r = new List<Recipes>();
            r.Add(new Recipes("Egg&Bacon", 10, "Cook in pan", 1, i));
            string expectedString = "Egg&Bacon";
            string expectedString2 = "Cook in pan";
            int expectedint = 10;
            int expectedint2 = 1;
            Assert.AreEqual(expectedString, r[0].name);
            Assert.AreEqual(expectedString2, r[0].method);
            Assert.AreEqual(expectedint, r[0].prep_time);
            Assert.AreEqual(expectedint2, r[0].Num_serves);
        }

        /// <summary>
        /// testing the binary file exists
        /// only works if file path is fully hard coded
        /// </summary>
        [TestMethod]
        public void FileExistsTestTrue()
        {
            FileInfo fi = new FileInfo(Path.GetFullPath(@"..\..\..\recipesApp\favourites.dat"));
            Assert.IsTrue(fi.Exists);
        }




    }

}
