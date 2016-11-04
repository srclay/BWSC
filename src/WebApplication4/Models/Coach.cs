using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BWSC.Models
{
    public class Coach
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public string FullName
        {
            get
            {
                return Surname + ", " + FirstName;
            }
        }
        public int SquadID { get; set; }
        public Squad Squad { get; set; }
    }
}
