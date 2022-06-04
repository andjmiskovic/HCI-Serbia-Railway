using System.Collections.Generic;

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
