using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Station> Stations { get; set; }
        public Train Train { get; set; }

        public Route()
        {
        }

        public Route(int id, string name, List<Station> stations, Train train)
        {
            Id = id;
            Name = name;
            Stations = stations;
            Train = train;
        }
    }
}
