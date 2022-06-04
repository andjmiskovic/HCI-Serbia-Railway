using System.Collections.Generic;

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
