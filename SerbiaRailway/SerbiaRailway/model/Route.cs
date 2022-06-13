using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Station> Stations { get; set; }
        public Train Train { get; set; }

        public string StationNames { get; set; }

        public Route()
        {
        }

        public Route(int id, string name, List<Station> stations, Train train)
        {
            Id = id;
            Name = name;
            Stations = stations;
            Train = train;
            setStationNames();
        }

        public void setStationNames()
        {
            StationNames = "";
            foreach (Station station in Stations)
            {
                StationNames += station + ", ";
            }
            StationNames = StationNames.Substring(0, StationNames.Length - 2);
        }
    }
}
