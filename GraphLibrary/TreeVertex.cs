using GraphLibrary;
using System;

namespace GraphTheory
{
    public class TreeVertex<T> : Vertex<T> where T : class, IComparable
    {
        public TreeVertex<T> LeftChild { get; set; }

        public TreeVertex<T> RightChild { get; set; }

        public bool HasNoChildren => 0 == GetNumberOfChildren();

        public bool HasOneChild => 1 == GetNumberOfChildren();

        public bool HasTwoChildren => 2 == GetNumberOfChildren();

        private int GetNumberOfChildren()
        {
            int result = 0;
            if (LeftChild != null) result++;
            if (RightChild != null) result++;
            return result;
        }

        public TreeVertex()
        {
            LeftChild = null;
            RightChild = null;
        }

        public TreeVertex(T data) : this()
        {
            Data = data;
        }
    }
}
