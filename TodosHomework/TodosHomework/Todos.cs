using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodosHomework
{
    public class Todos
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [StringLength(100)]
        public string Task { get; set; }

        [Required]
        public int Difficulty { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public enum StatusEnum { Pending = 1, Done = 2, Delegated = 3}

        
        public Todos(int id, string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            Id = id;
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
        }

        public static bool IsTaskValid(string task, out string error)
        {
            if (task.Length < 1 || task.Length > 100)
            {
                error = "Task must be 1-100 charcarters long";
                return false;
            }
            // If you declare a variable as output variable it MUST be assigned on all paths. So have to put it as something (null).
            // because there is no error here and it still needs to be assinged somehting it gets NULL
            error = null;
            return true;
        }







    }
}
