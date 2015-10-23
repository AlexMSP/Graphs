using System;
using System.Collections.Generic;

namespace GraphTheory
{
    enum VertexState
    {
        NotVisited,
        Visiting,
        Visited
    }

    public class Vertex<T> : IComparable where T : class, IComparable
    {
        public T Data { get; set; }

        public Dictionary<GraphVertex<T>, int> Costs { get; set; }

        public int CompareTo(Vertex<T> other)
        {
            return Data.CompareTo(other.Data);
        }

        public static bool operator <(Vertex<T> first, Vertex<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static bool operator >(Vertex<T> first, Vertex<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public override string ToString()
        {
            return Data.ToString();
        }

        public int CompareTo(object obj)
        {
            Vertex<T> vertex = obj as Vertex<T>;
            return CompareTo(vertex);
        }
    }
}
