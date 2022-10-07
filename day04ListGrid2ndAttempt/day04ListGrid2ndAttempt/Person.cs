using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// THIS IS THE DATA LAYER

// Instead of using ToString in Person.cs we are using a binding in gridview of XAML

namespace day04ListGrid2ndAttempt
{
    internal class Person
    {
        // Give it properties (CANNOT BE FIELD)
        public string Name { get; set; }

        public int Age { get; set; }

        // Right click --> quick actions & refactoring --> generate constructor
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Validation done in (control layer) of application --> but rules are still given by data layer 
        //DIFFERENT from using setters for validation --> this is a design CHOICE
        // Will call these methods in code behind to permofm validation on an object before creating it. 
        // Validators are STATIC --> Means do NOT belong to object but to CLASS --> Means can call them using the name of class (PERSON)
        // Similar to TryParse here --> output value string error and returns a boolean also
        public static bool IsNameValid(string name, out string error)
        {
            if (name.Length < 2 || name.Length > 50 || name.Contains(";"))
            {
                error = "Name must be 2-50 charcarters long, no semicolons";
                return false;
            }
            // If you declare a variable as output variable it MUST be assigned on all paths. So have to put it as something (null).
            // because there is no error here and it still needs to be assinged somehting it gets NULL
            error = null;
            return true;
        }

        // Same as above 
        public static bool IsAgeValid(int age, out string error)
        {
           if (age < 0 || age > 150)
            {
                error = "Age must be 0-150";
                return false;
            }
            error = null;
            return true;
        }


        // OVERLOAD method above
        // does the tryParse validation in here directly
        public static bool IsAgeValid(string strAge, out string error)
        {
            if (!int.TryParse(strAge, out int age))
            {
                error = "Age must be an integer value";
                return false;
            }
            if (age < 0 || age > 150)
            {
                error = "Age must be 0-150";
                return false;
            }
            error = null;
            return true;
        }

    }
}
