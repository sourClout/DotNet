using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    internal class Person
    {
        //Default constructor
        // public Person(){
        //Can also have default values
        //Name = "anonymous";
        //Age = 1;
        //City = "";
        // }

        //Constructor with parameters
        public Person(string name, int age, string city)
        {
            // this becomes a call to the setter method now --> no change to the syntax
            Name = name;
            Age = age;
            City = city;
        }

        // Has 3 FIELDS
        // Turn a field into a property with getters and setters

        // Need _name field for storage the moment you turn a field into a property --> Name no longer a field, just a gateway to call a getter/setter.
        // Field is private because cannot be accessed only by getters and setters
        // _name is industry standard if there are fields that should NOT be accessed directly (only by getters and setters)
        private string _name;

        public string Name {
            get { return _name; }
            set { 
                if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                {
                    throw new ArgumentException("Name must be 2-100 characters long, no semicolons");
                }
                _name = value;
            }
        }
        // Age 0-150
        private int _age; // storage for Age

        public int Age
        {
            get { return _age; }
            set {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentException("Age must be 0-150");
                }
                _age = value; }
        }


        private string _city;

        public string City // City 2-100 characters long, no semicolons
        {
            get { return _city; }
            set {
                //if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                if (!Regex.IsMatch(value, @"^[^;]{2,100}")) //, RegexOptions.IgnorCase))
                {
                    throw new ArgumentException("Name must be 2-100 characters long, no semicolons");
                }
                  _city = value; }
        }

        // ToString allows us to print things out more nicely.
        public override string ToString()
        {
            return $"{Name} is {Age} from {City}";
        }

    }
}
