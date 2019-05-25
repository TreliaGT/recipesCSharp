using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesApp.Classes
{
    class ingredients
    {
        private string ingredient;
        private string amount;

        public ingredients(string Ingredient, string Amount)
        {
            this.ingredient = Ingredient;
            this.amount = Amount;
        }


        //setters and getters
        public string getIngredient()
        {
            return ingredient;
        }

        public void setIngredient(string value)
        {
            ingredient = value;
        }

        public string getAmount()
        {
            return amount;
        }

        public void setAmount(string value)
        {
            amount = value;
        }
    }
}
