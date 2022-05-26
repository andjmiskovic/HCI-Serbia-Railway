using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class Schedule
    {
        public Line Line { get; set; }
        public DateTime DepartureDate { get; set; }

        public Schedule()
        {
        }

        public Schedule(Line line, DateTime departureDate)
        {
            Line = line;
            DepartureDate = departureDate;
        }
    }
}
