using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTheory
{
    public class GraphVertex<T> : Vertex<T>
           where T : class, IComparable
    {
        public List<GraphVertex<T>> Neighbours { get; }

        public bool IsConnectedTo(GraphVertex<T> other)
        {
            return Neighbours.Contains(other);
        }

        public bool HasTwoWayConnectionWith(GraphVertex<T> other)
        {
            return IsConnectedTo(other) && other.IsConnectedTo(this);
        }

        public GraphVertex()
        {
            Neighbours = new List<GraphVertex<T>>();
            Costs = new Dictionary<GraphVertex<T>, int>();
        }

        public GraphVertex(T data)
           : this()
        {
            Data = data;
        }

        public void Display()
        {
            var builder = new StringBuilder();
            if (Neighbours.Count > 0)
            {
                for (int index = 0; index < Neighbours.Count - 1; index++)
                {
                    var neighbor = Neighbours[index];
                    builder.Append(neighbor).Append(' ');
                }
                builder.Append(Neighbours[Neighbours.Count - 1]);
            }
            Console.WriteLine($"{this} -> {builder}");
        }
    }
}