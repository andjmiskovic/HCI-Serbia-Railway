using Microsoft.Maps.MapControl.WPF;

namespace SerbiaRailway.model
{
    // ima stanice, na primer Beograd Centar
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Location Location { get; set; }

        public Station()
        {
        }

        public Station(int id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
