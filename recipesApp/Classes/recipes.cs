using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesApp.Classes
{
    [Serializable]
    class Recipes 
    {
        private string Name;
        private int Prep_time;
        private string Method;
        private int num_serves;
        List<ingredients> list = new List<ingredients>();

   
        public Recipes(string name, int prep_time, string method, int Num_Serves, List<ingredients> List)
        {
            this.Name = name;
            this.Prep_time = prep_time;
            this.Method = method;
            this.num_serves = Num_Serves;
            this.list = List;
        }



        //setters and getters
        public string getName()
        {
            return Name;
        }

        public void setName(string value)
        {
            Name = value;
        }

        public int getPrep_time()
        {
            return Prep_time;
        }

        public void setPrep_time(int value)
        {
            Prep_time = value;
        }
        public string getMethod()
        {
            return Method;
        }

        public void setMethod(string value)
        {
            Name = Method;
        }
        public int getnum_serves()
        {
            return num_serves;
        }

        public void setnum_serves(int value)
        {
            num_serves = value;
        }

        public List<ingredients> getingredients()
        {
            return list;
        }

        public void setingredientss(List<ingredients> value)
        {
            list = value;
        }

    }
}
