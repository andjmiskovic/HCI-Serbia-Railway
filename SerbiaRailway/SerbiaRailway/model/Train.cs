using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class Train
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public List<Wagon> Wagons { get; set; }
        public bool IsActive { get; set; }

        public Train()
        {
        }

        public Train(int id, string manufacturer, List<Wagon> wagons, bool isActive)
        {
            Id = id;
            Manufacturer = manufacturer;
            Wagons = wagons;
            IsActive = isActive;
        }

        public override string ToString()
        {
            return Manufacturer;
        }

        public double getExtraPrice()
        {
            foreach (Seat seat in Wagons[0].Seats)
            {
                if (seat.Type == SeatType.FIRST_CLASS)
                    return seat.ExtraPrice;
            }
            return 0;
        }
    }
}
