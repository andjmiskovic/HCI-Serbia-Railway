namespace SerbiaRailway.model
{
    // ima stanice, na primer Beograd Centar
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Station()
        {
        }

        public Station(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
