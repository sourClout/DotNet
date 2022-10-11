using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MidTermAlexHolowach
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Destination { get; set; }

        [Required]
        [StringLength(30)]
        public string TravellerName { get; set; }

        [Required]
        [StringLength(8)]
        public string TravellerPassport { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public static bool IsDestinationValid(string destination, out string error)
        {
            if (destination.Length < 2 || destination.Length > 30)
            {
                error = "Destination must be 2-30 charcarters long";
                return false;
            }

            error = null;
            return true;
        }

        public static bool IsTravellerNameValid(string travellerName, out string error)
        {
            if (travellerName.Length < 2 || travellerName.Length > 30)
            {
                error = "Traveller name must be 2-30 charcarters long";
                return false;
            }

            error = null;
            return true;
        }

       

        public static bool IsReturnDateValid(DateTime returnDate, DateTime departureDate, out string error)
        {
            if (returnDate.Date < departureDate.Date)
            {
                error = "Return date must be after departure date";
                return false;
            }

            error = null;
            return true;
        }



    }
}
