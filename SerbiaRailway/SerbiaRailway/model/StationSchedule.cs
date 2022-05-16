using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    // vožnja od jedne do druge stanice, u koliko sati svakog dana se kreće i odakle i stigne i gde
    class StationSchedule
    {
        public Station StartingStation { get; set; }
        public Station EndStation { get; set; }
        public TimeSpan Departure { get; set; }
        public TimeSpan Arrival { get; set; }
        public double TravelTime { get; set; }
    }
}
