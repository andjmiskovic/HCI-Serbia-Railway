using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    class TrainStations
    {
        public List<Station> Stations { get; set; }

        public TrainStations()
        {
        }

        public TrainStations(List<Station> stations)
        {
            Stations = stations;
        }
    }
}
