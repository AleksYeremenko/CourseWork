using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Випускна
{
    class Ribs
    {
        public Peak ConnectedVertex { get; }
        public int EdgeWeight { get; }
        public Ribs(Peak connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }
}
