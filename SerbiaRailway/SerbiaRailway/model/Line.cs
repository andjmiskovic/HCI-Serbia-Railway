using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    // linija ima svoje ime, voz koji ide i listu svih među stanica zajedno sa satnicom
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StationSchedule> StationSchedule { get; set; }
        public Train Train { get; set; }

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

        public Line(int id, string name, List<StationSchedule> stationSchedule, Train train)
        {
            Id = id;
            Name = name;
            StationSchedule = stationSchedule;
            Train = train;
        }
    }
}
