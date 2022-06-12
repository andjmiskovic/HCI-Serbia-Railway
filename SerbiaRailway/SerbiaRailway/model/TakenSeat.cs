using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class TakenSeat
    {
        public Seat seat { get; set; }
        public bool isAvailable { get; set; }

        public TakenSeat(Seat seat, bool isAvailable)
        {
            this.seat = seat;
            this.isAvailable = isAvailable;
        }
    }
}
