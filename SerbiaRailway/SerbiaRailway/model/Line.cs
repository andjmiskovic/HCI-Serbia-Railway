using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    // linija ima svoje ime, voz koji ide i listu svih među stanica zajedno sa satnicom i cenom
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StationSchedule> StationSchedule { get; set; }
        public Train Train { get; set; }

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

        public Line()
        {
        }

        public Line(int id, string name, List<StationSchedule> stationSchedule, Train train)
        {
            Id = id;
            Name = name;
            StationSchedule = stationSchedule;
            Train = train;
        }
    }
}
