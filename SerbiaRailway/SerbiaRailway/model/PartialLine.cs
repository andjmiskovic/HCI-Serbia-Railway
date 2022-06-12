using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    // deo linije, od jedne stranice do druge
    public class PartialLine
    {
        public Station Start { get; set; }
        public Station End { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Line Line { get; set; }

        public PartialLine(Station start, Station end, TimeSpan startTime, TimeSpan endTime, Line line)
        {
            Start = start;
            End = end;
            StartTime = startTime;
            EndTime = endTime;
            Line = line;
        }

        public double CalculatePrice()
        {
            double price = 0;
            bool startingFound = false;
            foreach (StationSchedule stationSchedule in Line.StationSchedules)
            {
                if (stationSchedule.StartingStation == Start & stationSchedule.EndStation == End)
                {
                    return stationSchedule.Price;
                }

                if (stationSchedule.StartingStation == Start & startingFound == false)
                {
                    price = stationSchedule.Price;
                    startingFound = true;
                }

                if (stationSchedule.EndStation == End & startingFound == true)
                {
                    price += stationSchedule.Price;
                    break;
                }
                else
                {
                    price += stationSchedule.Price;
                }
            }
            return price;
        }
    }
}
