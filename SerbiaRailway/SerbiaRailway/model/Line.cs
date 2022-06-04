using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace SerbiaRailway.model
{
    // linija ima svoje ime, voz koji ide i listu svih među stanica zajedno sa satnicom
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StationSchedule> StationSchedule { get; set; }
        public Train Train { get; set; }
        public string Location { get; set; }

        // naredne ne ulaze u konstruktor ali su korisni u zadatku

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

        public Line()
        {
        }

        public Line(int id, string name, List<StationSchedule> stationSchedule, Train train, string location)
        {
            Id = id;
            Name = name;
            StationSchedule = stationSchedule;
            Train = train;
            Location = location;
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

        public List<Location> getAllLocationsFromLine(Line line)
        {
            List<string> locationsStrings = new List<string>();
            foreach (var station in line.StationSchedule)
            {
                if (!locationsStrings.Contains(station.StartingStation.Location))
                {
                    locationsStrings.Add(station.StartingStation.Location);
                }

                if (!locationsStrings.Contains(station.EndStation.Location))
                {
                    locationsStrings.Add(station.EndStation.Location);
                }
            }

            List<Location> locations = new List<Location>();
            foreach (string lok in locationsStrings)
            {
                locations.Add(createLocationFromString(lok));
            }

            return locations;
        }

        public Location createLocationFromString(string location)
        {
            Location Location = new Location(Convert.ToDouble(location.Split(',')[0]), Convert.ToDouble(location.Split(',')[1]));
            return Location;        
        }


    }
}
