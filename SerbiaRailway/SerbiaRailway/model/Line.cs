using System;
using System.Collections.Generic;
using System.Linq;

namespace SerbiaRailway.model
{
    // linija ima svoje ime, voz koji ide i listu svih među stanica zajedno sa satnicom i cenom
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StationSchedule> StationSchedule { get; set; }
        public Train Train { get; set; }
        public Dictionary<string, bool> WeekDays { get; set; }

        /////////////////////////////////

        public string StationStr { get; set; }
        public string TrainName { get; set; }
        public double Traveling { get; set; }

        public Station FirstStation()
        {
            return StationSchedule.ElementAt(0).StartingStation;
        }

        public Station LastStation()
        {
            return StationSchedule.ElementAt(-1).EndStation;
        }

        // ukupno vreme koliko voz putuje od prve do poslednje stanice
        public double TravelTime()
        {
            return StationSchedule.Sum(item => item.TravelTime);
        }

        public string GetStationString()
        {
            if (StationSchedule.Count() == 1) // ima samo jedna stanica
            {
                return StationSchedule.ElementAt(0).StartingStation.Name + "," + StationSchedule.ElementAt(0).EndStation.Name;
            }

            int i = 1;
            string retVal = FirstStation().Name + ",";
            while (i < StationSchedule.Count())
            {
                retVal += StationSchedule.ElementAt(i).StartingStation.Name + "," + StationSchedule.ElementAt(i).EndStation.Name;
                i++;
            }

            return retVal;
        }

        public TimeSpan GetStartingTimeByStation(Station station)
        {
            foreach (StationSchedule stationSchedule in StationSchedule)
            {
                if (station == stationSchedule.StartingStation)
                {
                    return stationSchedule.Departure;
                }
            }
            TimeSpan ts = new TimeSpan(0, 0, 0);
            return ts;
        }

        public TimeSpan GetEndingTimeByStation(Station station)
        {
            foreach (StationSchedule stationSchedule in StationSchedule)
            {
                if (station == stationSchedule.EndStation)
                {
                    return stationSchedule.Arrival;
                }
            }
            TimeSpan ts = new TimeSpan(0, 0, 0);
            return ts;
        }

        public Line()
        {
        }

        public Line(int id, string name, List<StationSchedule> stationSchedule, Train train)
        {
            Id = id;
            Name = name;
            StationSchedule = stationSchedule;
            Train = train;
            WeekDays = new Dictionary<string, bool>();
        }

        public Line(int id, string name, List<StationSchedule> stationSchedule, Train train, Dictionary<string, bool> weekDays)
        {
            Id = id;
            Name = name;
            StationSchedule = stationSchedule;
            Train = train;
            WeekDays = weekDays;
        }

        internal double calculatePriceByTwoStation(Station startingStation, Station endingStation)
        {
            double price = 0;
            bool startingFound = false;
            foreach(StationSchedule stationSchedule in StationSchedule)
            {
                if (stationSchedule.StartingStation == startingStation & stationSchedule.EndStation == endingStation)
                {
                    return stationSchedule.Price;
                }

                if (stationSchedule.StartingStation == startingStation & startingFound == false)
                {
                    price = stationSchedule.Price;
                    startingFound = true;
                }

                if (stationSchedule.EndStation == endingStation & startingFound == true)
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
