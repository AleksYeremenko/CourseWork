using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Випускна
{
    class Algoritm
    {
        Matrix graph;

        List<PropertyPeak> infos;
        public Algoritm(Matrix graph)
        {
            this.graph = graph;
        }
        void InitInfo()
        {
            infos = new List<PropertyPeak>();
            foreach (var v in graph.Vertices)
            {
                infos.Add(new PropertyPeak(v));
            }
        }
        PropertyPeak GetPeakProperty(Peak v)
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }
        public PropertyPeak FindUnvisitedVertexWithMinSum()//поиск среди непосещенных вершин вершину с минимальным весом
        {
            var minValue = int.MaxValue;
            PropertyPeak minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }
        public string FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }
        public string FindShortestPath(Peak startVertex, Peak finishVertex)
        {
            InitInfo();
            var first = GetPeakProperty(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }
        void SetSumToNextVertex(PropertyPeak info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetPeakProperty(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }
        string GetPath(Peak startVertex, Peak endVertex)
        {
            var path = endVertex.ToString();
            while (startVertex != endVertex)
            {
                endVertex = GetPeakProperty(endVertex).PreviousVertex;
                path = endVertex.ToString() + path;
            }

            return path;
        }
    }
}
