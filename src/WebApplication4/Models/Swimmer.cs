using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BWSC.Models
{
    public class Swimmer
    {
        public int ID { get; set; }
        public String Surname { get; set; }
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Display(Name = "ASA Number")]
        public String ASANumber { get; set; }
        public byte[] photo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of birth")]
        public DateTime DOB { get; set; }
        [Display(Name = "Start Date")]
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
