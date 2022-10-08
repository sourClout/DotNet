using System;
using System.Data.Entity;
using System.Linq;

namespace Doy05FirstEFConsole
{
    public class SocietyDbContext : DbContext
    {
        // Your context has been configured to use a 'SocietyDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Doy05FirstEFConsole.SocietyDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SocietyDbContext' 
        // connection string in the application configuration file.
        public SocietyDbContext()
            : base("name=SocietyDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        //How we create a table in the db
        // Table is called People, and will reflect the structure of Person class
        // If have multiple tables, will have multiple lines as below
        // Can add entities at bottom of this page, but better practice as we did is to put in separate file
        public virtual DbSet<Person> People { get; set; }

        // for Unique index --> programmtic way of creating a unique index
        //protected override void OnModelCreating(DbModelBuilder builder)
        //{
        //    builder.Entity<User>()
        //        .HasIndex(u => u.Email)
        //        .IsUnique();
        //}
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

   
    

   
}