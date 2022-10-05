using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    internal class Program
    {
        // since DataFile not a local variable it has capital not lower (in .Net only parameters and local variables are lower case)
        const string DataFileName = @"..\..\people.txt";
        // Provides storage for Person list (Uppercase People because not a local variable --> it is a field)
        static List<Person> People = new List<Person>();

        // Menu handle --> Menu choice. Print out the menu and get the user to choose something
        private static int getMenuChoice()
        {
            Console.Write("What do you want to do?\n" +
                    "1. Add person info\n" +
                    "2. List person info\n" +
                    "3. Find and List a person by name\n" +
                    "4. Find and list persons younger than age\n" +
                    "0. Exit\n" + "Choice: ");
            return(int.TryParse(Console.ReadLine(), out int choice)) ? choice : -1;
            // if the choice is true then returns the choice, if not retruns -1
        }

        // ADD PERSON
        private static void AddPersonInfo()
        {
            try
            {
                Console.WriteLine("Adding a person.");
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                Console.Write("Enter age: ");
                int age = int.Parse(Console.ReadLine()); // ex FormatException, OverflowException
                Console.Write("Enter city: ");
                string city = Console.ReadLine();
                Person person = new Person(name, age, city); // ex ArgumentException
                People.Add(person);
                Console.WriteLine("Person added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine("Error: invalid numerical input");
            }
        }

        //LIST ALL PEOPLE
        private static void ListAllPersonsInfo()
        {
            Console.WriteLine("Listing all persons");
            foreach (Person person in People)
            {
                Console.WriteLine(person);
                //Console.WriteLine(person.ToString); //--> Same as Code above since ToString automatic
            }
        }

        // READ ALL PEOPLE FROM FILE
        private static void ReadAllPeopleFromFile()
        {
            try
            {
                if (!File.Exists(DataFileName))
                {
                    return; // it's okay if the file does not exist yet
                }
                string[] linesArray = File.ReadAllLines(DataFileName); // ex IOException, SystemException
                foreach (string line in linesArray)
                {
                    try
                    {
                        string[] data = line.Split(';');
                        if (data.Length != 3)
                        {
                            throw new FormatException("Invalid number of items");
                            // or: Console.WriteLine("Error..."); continue;
                        }
                        string name = data[0];
                        int age = int.Parse(data[1]); // ex FormatException
                        string city = data[2];
                        Person person = new Person(name, age, city); // ex ArgumentException
                        People.Add(person);
                    }
                    catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
                    {
                        Console.WriteLine($"Error (skipping line): {ex.Message} in:\n  {line}");
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        // SAVE PEOPLE TO FILE
        private static void SaveAllPeopleToFile()
        {
            try
            {
                List<string> linesList = new List<String>();
                foreach (Person person in People)
                {
                    linesList.Add($"{person.Name};{person.Age};{person.City}");
                }
                File.WriteAllLines(DataFileName, linesList); // ex IOException, SystemException
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }

        private static void FindPersonYoungerThan()
        {
            Console.Write("Enter maximum age: ");
            if (!int.TryParse(Console.ReadLine(), out int maxAge))
            {
                Console.WriteLine("Invalid input");
                return;
            }
            // using a foreach loop with a condition
            Console.WriteLine("Peple at that age or younger:");
            foreach (Person p in People)
            {
                if (p.Age <= maxAge)
                {
                    Console.WriteLine(p);
                }
            }
            // using LINQ (Can use either solution)
            Console.WriteLine("Peple at that age or younger (using LINQ):");
            var youngerList = People.Where(p => (p.Age <= maxAge));
            foreach (Person p in youngerList)
            {
                Console.WriteLine(p);
            }
        }

        private static void FindPersonByName()
        {
            Console.Write("Enter partial person name: ");
            string searchStr = Console.ReadLine();
            // Lambda returns true/false --> if true, Lambda goes on final list (ToList) --> gets a list as result of search
            var matchesList = People.Where(p => p.Name.Contains(searchStr)).ToList(); // LINQ
            // var is same as List<Person> in this case --> Var is type which is discovered by the compiler, so you dont have to type it.
            if (matchesList.Count() > 0)
            {
                Console.WriteLine("Matches found:");
                foreach (Person p in matchesList)
                {
                    Console.WriteLine(p);
                }
            }
            else
            {
                Console.WriteLine("No matches found");
            }
        }

        static void Main(string[] args)
        {
            ReadAllPeopleFromFile();
            while (true)
            {
                int choice = getMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddPersonInfo();
                        break;
                    case 2:
                        ListAllPersonsInfo();
                        break;
                    case 3:
                        //throw new NotImplementedException();
                        FindPersonByName();
                        break;
                    case 4:
                        //throw new NotImplementedException();
                        FindPersonYoungerThan();
                        break;
                    case 0:
                        SaveAllPeopleToFile();
                        return; // exit the program
                    default:
                        Console.WriteLine("No such option, try again");
                        break;
                }
                Console.WriteLine();
            }
            /*
            try
            {
                try
                {
                    //Object Initializer --> NOT a constructor (dont need one). But can lead to cases where objects partially initialized. Constructor in class preferred.  
                    //Person p1 = new Person { Name = "Jerry", Age = 33, City = "New Jersey"};

                    // Since there is a constructor with Params Person p1 needs some initialization information
                    Person p1 = new Person("Jerry1", 33, "Montreal1");
                    Console.WriteLine($"Person: {p1.Name}, {p1.Age}, {p1.City}");
                }
                catch (ArgumentException ex)
                {
                    // Since new Person params calls the getter method it must catch the possible error from the Person class.
                    // in professional setting would write it to a log report for programmer to view bugs later and get trace of error messages. 
                    Console.WriteLine("Argument invlaid: " + ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    // look for stack trace lines that refer to your code (NOT from library)
                }
            }
            finally
            {
                Console.ReadKey();
            }*/



        }
    }
}
