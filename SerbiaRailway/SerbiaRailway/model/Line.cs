using System;
using System.Collections.Generic;
using System.Linq;

namespace SerbiaRailway.model
{
    // linija ima svoje ime, voz koji ide i listu svih među stanica zajedno sa satnicom i cenom
    public class Line
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public Dictionary<string, bool> WeekDays { get; set; }
        public List<StationSchedule> StationSchedules { get; set; }

        /////////////////////////////////

        public string StationStr { get; set; }
        public string TrainName { get; set; }
        public double Traveling { get; set; }

        public Station FirstStation()
        {
            return Route.Stations.ElementAt(0);
        }

        // ukupno vreme koliko voz putuje od prve do poslednje stanice
        public double TravelTime()
        {
            return StationSchedules.Sum(item => item.TravelTime);
        }

        public Station LastStation()
        {
            return Route.Stations.ElementAt(-1);
        }


        public string GetStationString()
        {
            if (Route.Stations.Count() == 1) // ima samo jedna stanica
            {
                return Route.Stations.ElementAt(0).Name + "," + Route.Stations.ElementAt(0).Name;
            }

            int i = 1;
            string retVal = FirstStation().Name + ",";
            while (i < Route.Stations.Count())
            {
                retVal += Route.Stations.ElementAt(i).Name + "," + Route.Stations.ElementAt(i).Name;
                i++;
            }
            return retVal;
        }

        public TimeSpan GetStartingTimeByStation(Station station)
        {
            foreach (StationSchedule stationSchedule in StationSchedules)
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
            foreach (StationSchedule stationSchedule in StationSchedules)
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

        public Line(int id, Route route, Dictionary<string, bool> weekDays, List<StationSchedule> stationSchedule)
        {
            Id = id;
            Route = route;
            WeekDays = weekDays;
            StationSchedules = stationSchedule;
        }

        internal double calculatePriceByTwoStation(Station startingStation, Station endingStation)
        {
            double price = 0;
            bool startingFound = false;
            foreach(StationSchedule stationSchedule in StationSchedules)
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
