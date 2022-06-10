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
    }
}
