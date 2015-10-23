using System;
using System.Linq;

namespace GraphLibrary
{
    public class DirectedGraph<T> : Graph<T> where T : class, IComparable
    {
        public override void AddEdge(GraphVertex<T> v1, GraphVertex<T> v2, int cost)
        {
            AddEdge(v1, v2);
            v1.Costs.Add(v2, cost);
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
        }

        public override void AddEdge(T data1, T data2)
        {
            AddEdge(VertexAt(data1), VertexAt(data2));
        }

        public override void RemoveEdge(T data1, T data2)
        {
            RemoveEdge(VertexAt(data1), VertexAt(data2));
        }

        public int InnerRank(GraphVertex<T> vertex)
        {
            return Vertices.Count(graphVertex => graphVertex.IsConnectedTo(vertex));
        }

        public int OuterRank(GraphVertex<T> vertex)
        {
            return vertex.Neighbours.Count;
        }

        public override bool IsIsolated(GraphVertex<T> vertex)
        {
            return OuterRank(vertex) + InnerRank(vertex) == 0;
        }

        public override bool IsTerminal(GraphVertex<T> vertex)
        {
            return OuterRank(vertex) == 0;
        }

        public override int[][] GetAdjacencyMatrix()
        {
            var result = new int[NumberOfVertices][];

            for (int i = 0; i < NumberOfVertices; i++)
            {
                result[i] = new int[NumberOfVertices];
            }


            for (int i = 0; i < NumberOfVertices; i++)
            {
                for (int j = 0; j < NumberOfVertices; j++)
                {
                    if (Vertices[i].IsConnectedTo(Vertices[j]))
                    {
                        result[i][j] = 1;
                    }
                    if (Vertices[j].IsConnectedTo(Vertices[i]))
                    {
                        result[j][i] = 1;
                    }
                }
            }
            return result;
        }

        public override void RemoveEdge(GraphVertex<T> v1, GraphVertex<T> v2)
        {
            v1?.Neighbours.Remove(v2);
        }

        public bool HasEdge(GraphVertex<T> v1, GraphVertex<T> v2)
        {
            return v1.IsConnectedTo(v2);
        }

        public bool HasAnyTwoWayEdge()
        {
            return Vertices.Any(vertex => vertex.Neighbours.Any(vertex.HasTwoWayConnectionWith));
        }

    }
}
