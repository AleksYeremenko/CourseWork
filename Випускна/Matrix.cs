using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Випускна
{
    class Matrix
    {
        public List<Peak> Vertices { get; }
        public Matrix()
        {
            Vertices = new List<Peak>();
        }
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new Peak(vertexName));
        }
        public Peak FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }
        public void AddEdge(string firstName, string secondName, int weight)//добавление веса к вершинам
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)//проверка на наличие дуг
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }
}
