using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerbiaRailway.services;

namespace SerbiaRailway.model
{
    public class Train
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public List<Seat> Seats { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfSeats { get; set; }

        public Train()
        {
        }

        public Train(int id, string manufacturer, List<Seat> seats, bool isActive)
        {
            Id = id;
            Manufacturer = manufacturer;
            Seats = seats;
            IsActive = isActive;
        }
    }
}
