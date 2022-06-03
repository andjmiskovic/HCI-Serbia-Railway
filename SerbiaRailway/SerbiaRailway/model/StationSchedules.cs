using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class StationSchedules
    {
        public List<StationSchedule> Schedules { get; set; }

        public StationSchedules()
        {
        }

        public StationSchedules(List<StationSchedule> schedules)
        {
            Schedules = schedules;
        }
    }
}
