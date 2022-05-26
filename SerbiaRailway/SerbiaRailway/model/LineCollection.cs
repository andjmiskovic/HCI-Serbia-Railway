using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
