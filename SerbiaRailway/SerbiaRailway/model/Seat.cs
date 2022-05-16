using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public enum SeatType
    {
        FIRST_CLASS,
        SECOND_CLASS
    }

    public class Seat
    {
        public int SeatNumber { get; set; }
        public SeatType Type { get; set; }
        public double Price { get; set; }
    }
}
