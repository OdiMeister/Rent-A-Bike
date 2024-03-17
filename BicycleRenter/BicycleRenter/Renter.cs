using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRenter
{
    internal class Renter
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime timeOfRent { get; set; }  
        public string renterIdNum { get; set; }
        public string bikeId { get; set; }
    }
}
