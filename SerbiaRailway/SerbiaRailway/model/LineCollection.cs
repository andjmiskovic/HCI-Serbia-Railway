using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class LineCollection
    {
        public List<Line> Lines { get; set; }

        public LineCollection()
        {
        }

        public LineCollection(List<Line> lines)
        {
            Lines = lines;
        }
    }
}
