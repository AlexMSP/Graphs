using GraphTheory;
using System;
using System.Collections.Generic;

namespace GraphLibrary
{
    public class BinaryTree<T> where T : class, IComparable
    {
        public int Count { get; set; }

        public TreeVertex<T> Root { get; set; }

        public bool IsEmpty => Root == null;

        public BinaryTree()
        {
            Root = null;
        }

        public BinaryTree(T data) : this()
        {
            Root = new TreeVertex<T>(data);
            Count = 1;
        }

        public BinaryTree(TreeVertex<T> root) : this(root.Data)
        {
        }

        public void DisplayInOrder()
        {
            var orderedList = new List<T>();
            DisplayInOrder(Root, orderedList);
            PrintHelpers.Print(orderedList, " ");
        }

        public void DisplayPreOrder()
        {
            var orderedList = new List<T>();
            DisplayPreOrder(Root, orderedList);
            PrintHelpers.Print(orderedList, " ");
        }

        public void DisplayPostOrder()
        {
            var orderedList = new List<T>();
            DisplayPostOrder(Root, orderedList);
            PrintHelpers.Print(orderedList, " ");
        }

        public void Add(T data)
        {
            if (Contains(data))
            {
                return;
            }
            var newVertex = new TreeVertex<T>(data);
            Count++;

            if (IsEmpty)
            {
                Root = newVertex;
                return;
            }

            var current = Root;
            var parent = Root;
            while (true)
            {
                if (current == null)
                {
                    if (newVertex < parent)
                    {
                        parent.LeftChild = newVertex;
                    }
                    else
                    {
                        parent.RightChild = newVertex;
                    }
                    return;
                }
                parent = current;
                current = newVertex < current ? current.LeftChild : current.RightChild;
            }
        }

        public bool RemoveRoot()
        {
            if (IsEmpty)
                return false;
            var success = Remove(Root.Data);
            if (success)
                Count--;
            return success;
        }

        public bool Remove(T data)
        {
            if (Root == null)
            {
                return false;
            }

            TreeVertex<T> current = Root;
            TreeVertex<T> parent = null;

            int result = current.Data.CompareTo(data);

            while (result != 0)
            {
                if (result > 0)
                {
                    parent = current;
                    current = current.LeftChild;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.RightChild;
                }

                if (current == null)
                    return false;

                result = current.Data.CompareTo(data);
            }

            if (current.RightChild == null)
            {
                if (parent == null)
                    Root = current.LeftChild;
                else
                {
                    result = parent.Data.CompareTo(current.Data);
                    if (result > 0)
                        parent.LeftChild = current.LeftChild;
                    else if (result < 0)
                        parent.RightChild = current.LeftChild;
                }
            }

            else if (current.RightChild.LeftChild == null)
            {
                current.RightChild.LeftChild = current.LeftChild;

                if (parent == null)
                    Root = current.RightChild;
                else
                {
                    result = parent.Data.CompareTo(current.Data);
                    if (result > 0)
                        parent.LeftChild = current.RightChild;
                    else if (result < 0)
                        parent.RightChild = current.RightChild;
                }
            }
            else
            {
                TreeVertex<T> leftMost = current.RightChild.LeftChild, leftMostParent = current.RightChild;
                while (leftMost.LeftChild != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.LeftChild;
                }

                leftMostParent.LeftChild = leftMost.RightChild;

                leftMost.LeftChild = current.LeftChild;
                leftMost.RightChild = current.RightChild;

                if (parent == null)
                    Root = leftMost;
                else
                {
                    result = parent.Data.CompareTo(current.Data);
                    if (result > 0)
                        parent.LeftChild = leftMost;
                    else if (result < 0)
                        parent.RightChild = leftMost;
                }
            }
            Count--;
            return true;
        }

        private bool Contains(T data, TreeVertex<T> current)
        {
            if (current == null) return false;

            if (data.CompareTo(current.Data) < 0)
                return Contains(data, current.LeftChild);
            if (data.CompareTo(current.Data) > 0)
                return Contains(data, current.RightChild);
            return true;
        }

        public bool Contains(T data)
        {
            return Contains(data, Root);
        }

        private void DisplayInOrder(TreeVertex<T> root, List<T> result)
        {
            if (root == null) return;
            DisplayInOrder(root.LeftChild, result);
            result.Add(root.Data);
            DisplayInOrder(root.RightChild, result);
        }

        private void DisplayPreOrder(TreeVertex<T> root, List<T> result)
        {
            if (root == null) return;
            result.Add(root.Data);
            DisplayPreOrder(root.LeftChild, result);
            DisplayPreOrder(root.RightChild, result);
        }

        private void DisplayPostOrder(TreeVertex<T> root, List<T> result)
        {
            if (root == null) return;
            DisplayPostOrder(root.LeftChild, result);
            DisplayPostOrder(root.RightChild, result);
            result.Add(root.Data);
        }

        public void Clear()
        {
            while (Count != 0)
            {
                RemoveRoot();
            }
        }
    }
}
