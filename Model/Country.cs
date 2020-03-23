using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaInfoAppCore.Model
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public IList<Case> Confirmed { get; set; }
        public IList<Case> Recovered { get; set; }
        public IList<Case> Deaths { get; set; }
    }
}
