using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWSC.Models
{
    public class Squad
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public ICollection<Swimmer> Swimmers { get; set; }
        public ICollection<Coach> Coaches { get; set; }
    }
}
