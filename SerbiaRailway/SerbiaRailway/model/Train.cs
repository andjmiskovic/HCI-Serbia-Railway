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
        public List<Seat> Seats { get; set; }
        public bool IsActive { get; set; }
    }
}
