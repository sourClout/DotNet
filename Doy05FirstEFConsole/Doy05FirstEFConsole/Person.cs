using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doy05FirstEFConsole
{
    public class Person
    {
        // If ID or PersonID it is assumed to be primary
        [Key]
        public int id { get; set; }

        [Required] // means not null
        [StringLength(50)] // nvarchar(50)
        public string name { get; set; }

        [Required]
        [Index] // not unique, speeds up lookup operations
        public int age { get; set; }

        [NotMapped]
        public string Comment { get; set; }

       


    }
}
