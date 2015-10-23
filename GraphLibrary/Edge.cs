using System;

namespace GraphTheory
{
    public class Edge<T> where T : class, IComparable
    {
        private readonly Vertex<T> _vertex1;
        private readonly Vertex<T> _vertex2;

        public Edge(Vertex<T> vertex1, Vertex<T> vertex2)
        {
            _vertex1 = vertex1;
            _vertex2 = vertex2;
        }

        public void Display()
        {
            Console.WriteLine($"{_vertex1.Data}->{_vertex2.Data}");
        }

        public bool IsAdjacent(Edge<T> other)
        {
            return _vertex1.Equals(other._vertex1) || _vertex1.Equals(other._vertex2) ||
                   _vertex2.Equals(other._vertex1) || _vertex2.Equals(other._vertex2);
        }
    }
}