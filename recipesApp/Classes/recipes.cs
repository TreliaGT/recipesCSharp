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
        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public int prep_time
        {
            get
            {
                return Prep_time;
            }
            set
            {
                Prep_time = value;
            }
        }

        public string method
        {
            get
            {
                return Method;
            }
            set
            {
                Method = value;
            }
        }

        public int Num_serves
        {
            get
            {
                return num_serves;
            }
            set
            {
                num_serves = value;
            }
        }

        public List<ingredients> Ingredients{
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
            }


    }
}
