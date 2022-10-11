using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TodoEnum
{
    public class Todos
    {
        [Key]
        public int Id { get; set; }

        // [RegularExpression(@"([A-Za-z0-9\s./,-;+)(*!])+")]
        [Required]
        [StringLength(100)]
        public string Task { get; set; }

        [Required]
        public double Difficulty { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public enum StatusEnum { Pending, Done, Delegated }


        /*
        public Todos(int id, string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            Id = id;
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
        }
        */

        /*
        public Todos()
        {
        }
        */

        public static bool IsTaskValid(string task, out string error)
        {
            /*
            if (task.Length < 1 || task.Length > 100)
            {
                error = "Task must be 1-100 charcarters long";
                return false;
            }
            */
           
            if (!Regex.IsMatch(task, @"([A - Za - z0 - 9\s./, -; +)(*!]{1,100})"))
            {
                error = "Task must be 1-100 characters long and only contain valid characters"; 
                return false;
            }
            
            // If you declare a variable as output variable it MUST be assigned on all paths. So have to put it as something (null).
            // because there is no error here and it still needs to be assinged somehting it gets NULL
            error = null;
            return true;
        }


        public static bool IsDateValid(DateTime dueDate, out string error)
        {
            if (dueDate.Year < 1900 || dueDate.Year > 2099)
            {
                error = "Date must be between years 1900 - 2099";
                return false;
            }
            error = null;
            return true;
        }
    }
}
