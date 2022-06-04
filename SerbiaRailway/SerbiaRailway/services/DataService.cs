using Microsoft.Maps.MapControl.WPF;
using SerbiaRailway.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.services
{
    public static class DataService
    {
        public static Data Data => Data.Instance;

        // nisam siguran da li ovo sme da stoji ovde ali radi poso za sad
        public static List<Location> getAllLocationsFromLine(Line line)
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

        public static Location createLocationFromString(string location)
        {
            Location Location = new Location(Convert.ToDouble(location.Split(',')[0]), Convert.ToDouble(location.Split(',')[1]));
            return Location;
        }
    }
}
