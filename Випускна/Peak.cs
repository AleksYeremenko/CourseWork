
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Випускна
{
    class Peak
    {
        public string Name { get; }
        public List<Ribs> Edges { get; }
        public Peak(string vertexName)
        {
            Name = vertexName;
            Edges = new List<Ribs>();
        }
        public void AddEdge(Ribs newEdge)
        {
            Edges.Add(newEdge);
        }
        public void AddEdge(Peak vertex, int edgeWeight)
        {
            AddEdge(new Ribs(vertex, edgeWeight));
        }
        public override string ToString() => Name;
    }
}
