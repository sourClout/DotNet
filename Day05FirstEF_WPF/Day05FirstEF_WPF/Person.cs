using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05FirstEF_WPF
{
    public class Person
    {
        // In entity framework we add attributes (ex. key)
        // every time we have a string we need to give it a limit on length

        // If ID or PersonID it is assumed to be primary
        [Key]
        public int Id { get; set; }

        [Required] // means not null
        [StringLength(50)] // nvarchar(50)
        public string Name { get; set; }

        [Required]
        [Index] // not unique, speeds up lookup operations --> dont really need it here (more important for unique values)
        public int Age { get; set; }

        /*
        [NotMapped]
        public string Comment { get; set; }
        */

        // For display purposes we want a property to be displayed that is not in the DB
        // EX: listview with gridview display and want to show a column, but should not be in the DB (EX: a summary or a count) but we DO need it as an attriubute of our class
        // Give attribute NOT MAPPED --> means in meomry only, not reflected in DB as column
        /*
        [NotMapped] // in memory only (eg. computed), not in database
        public string comment { get; set; }
        */


        // How to doing an enum in entity framework
        /*
        [EnumDataType(typeof(GenderEnum))]
        public GenderEnum Gender { get; set; }
        public enum GenderEnum { Male = 1, Female = 2, Other = 4, NA = 3}
        */

    }
}
