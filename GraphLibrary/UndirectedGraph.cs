using System;

namespace GraphLibrary
{
    public class UndirectedGraph<T> : Graph<T> where T : class, IComparable
    {
        public override void AddEdge(GraphVertex<T> v1, GraphVertex<T> v2, int cost)
        {
            AddEdge(v1, v2);
            v1.Costs.Add(v2, cost);
            v2.Costs.Add(v1, cost);
        }

        public override void AddEdge(T data1, T data2, int cost)
        {
            AddEdge(VertexAt(data1), VertexAt(data2), cost);
        }

        public override void AddEdge(GraphVertex<T> v1, GraphVertex<T> v2)
        {
            if (!Vertices.Contains(v1) || !Vertices.Contains(v2)) return;
            if (v1.Neighbours.Contains(v2)) return;
            v1.Neighbours.Add(v2);
            if (v2.Neighbours.Contains(v1)) return;
            v2.Neighbours.Add(v1);
        }

        public override void AddEdge(T data1, T data2)
        {
            AddEdge(VertexAt(data1), VertexAt(data2));
        }

        public override void RemoveEdge(GraphVertex<T> v1, GraphVertex<T> v2)
        {
            v1?.Neighbours.Remove(v2);
            v2?.Neighbours.Remove(v1);
        }

        public override void RemoveEdge(T data1, T data2)
        {
            RemoveEdge(VertexAt(data1), VertexAt(data2));
        }

        public override bool IsIsolated(GraphVertex<T> graphVertex)
        {
            return graphVertex.Neighbours.Count == 0;
        }

        public override bool IsTerminal(GraphVertex<T> vertex)
        {
            return vertex.Neighbours.Count == 1;
        }

        public override int[][] GetAdjacencyMatrix()
        {
            var result = new int[NumberOfVertices][];
            for (int i = 0; i < NumberOfVertices; i++)
            {
                result[i] = new int[NumberOfVertices];
                for (int j = 0; j < NumberOfVertices; j++)
                {
                    if (!Vertices[i].IsConnectedTo(Vertices[j])) continue;
                    result[i][j] = 1;
                    result[j][i] = 1;
                }
            }
            return result;
        }
    }
}