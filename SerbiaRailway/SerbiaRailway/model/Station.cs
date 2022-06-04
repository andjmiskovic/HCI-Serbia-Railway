using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    // ima stanice, na primer Beograd Centar
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public Station()
        {
        }

        public Station(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = Location;
        }
    }
}
