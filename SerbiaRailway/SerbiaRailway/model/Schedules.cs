using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class Schedules
    {
        public List<Schedule> TravelSchedules { get; set; }

        public Schedules()
        {
        }

        public Schedules(List<Schedule> travelSchedules)
        {
            TravelSchedules = travelSchedules;
        }
    }
}
