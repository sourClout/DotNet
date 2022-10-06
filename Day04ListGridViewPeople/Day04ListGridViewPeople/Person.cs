using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04ListGridViewPeople
{
    internal class Person

    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static bool IsNameValid(string name)
        {
            return !(name.Length < 2 || name.Length > 50 || name.Contains(";"));
        }

        public static bool IsAgeValid(int age)
        {
            return !(age < 0 || age > 150);
        }
    }
}
