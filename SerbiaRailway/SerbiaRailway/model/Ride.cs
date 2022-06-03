using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class Ride
    {
        // gleda se samo datum
        public DateTime Date { get; set; }
        public Line Line { get; set; }
        public List<Ticket> Tickets { get; set; }

        public Ride(DateTime date, Line line)
        {
            this.Line = line;
            this.Date = date;
            this.Tickets = new List<Ticket>();
        }
    }
}
