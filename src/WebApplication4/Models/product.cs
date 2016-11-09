using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWSC.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public string ImageFileName { get; set; }
    }
}
