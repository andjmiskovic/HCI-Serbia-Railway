using System;

namespace SerbiaRailway.model
{
    // vožnja od jedne do druge stanice, u koliko sati svakog dana se kreće i odakle i stigne i gde
    public class StationSchedule
    {
        public Station StartingStation { get; set; }
        public Station EndStation { get; set; }
        public TimeSpan Departure { get; set; }
        public TimeSpan Arrival { get; set; }
        public double TravelTime { get; set; }
        public double Price { get; set; }

        public StationSchedule()
        {
        }

        public StationSchedule(Station startingStation, Station endStation, TimeSpan departure, TimeSpan arrival, double price)
        {
            StartingStation = startingStation;
            EndStation = endStation;
            Departure = departure;
            Arrival = arrival;
            TravelTime = (Arrival - Departure).TotalMinutes;
            Price = price;
        }
    }
}
