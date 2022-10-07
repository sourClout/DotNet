using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04ListGridViewPeople
{
    internal class Person

    {
        // must be properties and NOT fields to allow binding in XAML
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static bool IsNameValid(string name, out string error)
        {
            if (name.Length < 2 || name.Length > 50 || name.Contains(";"))
            {
                error = "name must be 2-50 characretrs long, no semicolons";
                return false;
            }
            error = null;
            return true;
        }

        public static bool IsAgeValid(string strAge, out string error)
        {
            if (!int.TryParse(strAge, out int age))
            {
                error = "Age must be an integer value";
                return false;
            }
            if (age < 0 || age > 150) {
                error = "Age must be 0-150";
                return false;
            }
            error = null;
            return true;
        }

        // Not actually used since other method above is used for string
        public static bool IsAgeValid(int age, out string error)
        {
            if (age < 0 || age > 150)
            {
                error = "Age must be 0-150";
                return false;
            }
            error=null;
            return true;
        }
    }
}
