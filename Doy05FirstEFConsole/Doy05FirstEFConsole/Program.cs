using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doy05FirstEFConsole
{
    // USING ENTITY FRAMEWORK --> Similar to Java JPA (Java persistence API)
    // 1. Add necessary Nugguet Package: Tools --> NuGet Package Manager --> Manage NuGet Packages for solution (Package Manager like NPM or Maven, additional libraries we can install) -->
    //  Browse --> EntityFramework --> select project on right hand side --> install + Accept (once installed will see additional libraries in references)
    // 2. Add database context: right click project name on right side panel --> add --> new item --> ADO.NET entity Data Model "SocietyDbContext" (will be managing Person so goes with Society)
    // Empty Code First Model (creating our classes representing rows and tables to database)
    // 3. modify line 24 in SocietyDbContext.cs
    // In Git make sure directory packages is ignored (already ignored)
    // 4. Create class Person --> must change person class to PUBLIC
    // These database frameworks are SYNCHRONIZATION FRAMEWORKS --> they synch whats in memory with whats in DB and allow you to Pull things from DB, modify them in memory and 
    // PUSH them back to DB without actually executing SQL queries.
    // HOW TO PROVE DATABASE IS INSERTED:
    // View --> sevrer explorer (can use to connect to SQL server. will do next week for project. TODAY just connect to local database) --> Data Connections (right click)
    // --> Add connection -->  Microsoft SQL Server Database File --> Continue --> Browse for File (in home directory) --> find table and show table data 
    // --> if run program again will see another record added
    // IF YOU EVER modify the fields of your class, add a new number to end of databse file name on app.config line catalogue="" (creates a new databse file each time) --> this avoids migrations
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                // Connect to DB by Instantiating the Context
                SocietyDbContext ctx = new SocietyDbContext();
                Random random = new Random();
                // INSERT: equivalent of insert
                // Instantiate an Object of new perosn Jerry with random number and age random number 
                Person p1 = new Person { Name = "Jerry " + random.Next(100), Age = random.Next(100) };
                // ADD them to collection
                ctx.People.Add(p1); // insert operation is scheduled but NOT executed yet
                // Save changes made
                ctx.SaveChanges(); // synchronize objects in memory with database
                Console.WriteLine("record added");

                // UPDATE: equivalent of update - fetch then modify, save changes
                // FirstOrDefault will either return Person or null
                // This in LINQ --> used on the context and this gets translated on backend into SQL and then executed.
                // Select all from context people wehre person id = 3 and give me that person.
                // First or default (default is always null) --> The LinQ select query can return 0, 1 or more records (practically it will either return 1 or 0)
                // First or Default says give me either the first record or default, which is a null (so p2 will either be null or not)
                // RECORDS p2 and p3 (from delete) are tracked by the entity framework ("attached")
                Person p2 = (from p in ctx.People where p.Id == 2 select p).FirstOrDefault<Person>();
                if (p2 != null)
                {
                    p2.Name = "Alabama " + (random.Next(10000) + 10000); // entity framework is watching and notices the modification
                    ctx.SaveChanges(); // update the database to synchronize entities in memory with the database
                    Console.WriteLine("Record updated");
                }
                else
                {
                    Console.WriteLine("record to update not found");
                }
                // delete - fetch then schedule for deletion, then save changes
                Person p3 = (from p in ctx.People where p.Id == 3 select p).FirstOrDefault<Person>();
                if (p3 != null)
                { // found the record to delete
                    ctx.People.Remove(p3); // schedule for deletion in the database
                    ctx.SaveChanges(); // update the database to synchronize entities in memory with the database
                    Console.WriteLine("Record deleted");
                }
                else
                {
                    Console.WriteLine("record to delete not found");
                }
                // fetch all records --> Can use other notation from LinQ interchangeably ctx.People.Where(p =?)
                List<Person> peopleList = (from p in ctx.People select p).ToList<Person>();
                foreach (Person p in peopleList)
                {
                    Console.WriteLine($"{p.Id}: {p.Name} is {p.Age} y/o");
                }
            }
            catch (SystemException ex) // catch-all for EF, SQL and many other exceptions
            {
                Console.WriteLine("Database operation failed: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }


        }
    }
}
