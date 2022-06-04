using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class TrainStations
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
