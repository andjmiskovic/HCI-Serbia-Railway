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
        public List<Wagon> Wagon { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfSeats { get; set; }

        public Train()
        {
        }

        public Train(int id, string manufacturer, List<Wagon> wagons, bool isActive)
        {
            Id = id;
            Manufacturer = manufacturer;
            Wagon = wagons;
            IsActive = isActive;
        }
    }
}
