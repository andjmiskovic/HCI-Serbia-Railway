using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
