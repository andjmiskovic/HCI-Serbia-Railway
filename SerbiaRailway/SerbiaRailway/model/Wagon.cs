using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class Wagon
    {
        public int Number { get; set; }
        public List<Seat> Seats { get; set; }

        public Wagon(int number, List<Seat> seats)
        {
            Number = number;
            Seats = seats;
        }
    }
}
