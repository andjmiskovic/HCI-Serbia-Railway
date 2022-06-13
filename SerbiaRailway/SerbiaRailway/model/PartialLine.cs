using System;

namespace SerbiaRailway.model
{
    // deo linije, od jedne stranice do druge
    public class PartialLine
    {
        public Station Start { get; set; }
        public Station End { get; set; }
        public Line Line { get; set; }

        public PartialLine(Station start, Station end, Line line)
        {
            Start = start;
            End = end;
            Line = line;
        }

        public TimeSpan StartTime()
        {
            foreach(StationSchedule schedule in Line.StationSchedules)
            {
                if (schedule.StartingStation == Start)
                    return schedule.Departure;
            }
            return new TimeSpan();
        }

        public TimeSpan EndTime()
        {
            foreach (StationSchedule schedule in Line.StationSchedules)
            {
                if (schedule.EndStation == End)
                    return schedule.Arrival;
            }
            return new TimeSpan();
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
