using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Випускна
{
    class PropertyPeak
    {
        public Peak Vertex { get; set; }
        public bool IsUnvisited { get; set; }
        public int EdgesWeightSum { get; set; }

        public Peak PreviousVertex { get; set; }
        public PropertyPeak(Peak vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }


    }
}
