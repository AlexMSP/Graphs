using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTheory
{
    public abstract class Graph<T> where T : class, IComparable
    {
        const int Infinity = int.MaxValue;

        public bool HasCycle { get; set; }

        public bool IsCompleted
        {
            get { return Vertices.All(vertex => vertex.Neighbours.Count == Vertices.Count - 1); }
        }

        public int NumberOfVertices { get; set; }

        public List<GraphVertex<T>> Vertices { get; set; }

        protected Graph()
        {
            Vertices = new List<GraphVertex<T>>();
        }

        public abstract void AddEdge(GraphVertex<T> v1, GraphVertex<T> v2, int cost);

        public abstract void AddEdge(T data1, T data2, int cost);

        public abstract void AddEdge(GraphVertex<T> v1, GraphVertex<T> v2);

        public abstract void AddEdge(T data1, T data2);

        public abstract void RemoveEdge(GraphVertex<T> v1, GraphVertex<T> v2);

        public abstract void RemoveEdge(T data1, T data2);

        public void RemoveVertex(GraphVertex<T> graphVertex)
        {
            if (!Vertices.Contains(graphVertex)) return;

            for (int i = 0; i < graphVertex.Neighbours.Count; i++)
            {
                graphVertex.Neighbours[i].Neighbours.Remove(graphVertex);
            }

            Vertices.Remove(graphVertex);
            NumberOfVertices--;
        }

        public void RemoveVertex(T data)
        {
            RemoveVertex(VertexAt(data));
        }

        public void AddVertex(GraphVertex<T> graphVertex)
        {
            if (Vertices.Contains(graphVertex)) return;
            Vertices.Add(graphVertex);
            NumberOfVertices++;
        }

        public void AddVertex(T data)
        {
            if (Vertices.Contains(VertexAt(data))) return;
            AddVertex(new GraphVertex<T>(data));
        }

        public void Display()
        {
            if (Vertices == null) return;
            foreach (var vertex in Vertices)
            {
                vertex.Display();
            }
        }

        public void Sort()
        {
            if (Vertices == null) return;
            Vertices.Sort();
            foreach (var vertex in Vertices)
            {
                vertex.Neighbours.Sort();
            }
        }

        public abstract bool IsIsolated(GraphVertex<T> graphVertex);

        public abstract bool IsTerminal(GraphVertex<T> vertex);

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var vertex in Vertices)
            {
                builder.Append(vertex);
            }
            return builder.ToString();
        }

        public GraphVertex<T> VertexAt(T data)
        {
            return Vertices.FirstOrDefault(x => x.Data.Equals(data));
        }

        public HashSet<GraphVertex<T>> TopologicalSort()
        {
            HashSet<GraphVertex<T>> queue = new HashSet<GraphVertex<T>>();
            Dictionary<GraphVertex<T>, VertexState> vertexStates = new Dictionary<GraphVertex<T>, VertexState>(NumberOfVertices);
            foreach (var vertex in Vertices)
            {
                vertexStates.Add(vertex, VertexState.NotVisited);
            }

            foreach (var vertex in Vertices)
            {
                vertexStates[vertex] = VertexState.Visiting;
                if (vertexStates[vertex] == VertexState.Visited)
                {
                    continue;
                }
                TopologicalSortHelper(vertex, queue, vertexStates);
                vertexStates[vertex] = VertexState.Visited;
            }
            return queue;
        }

        public abstract int[][] GetAdjacencyMatrix();

        private void TopologicalSortHelper(GraphVertex<T> graphVertex, HashSet<GraphVertex<T>> stack, Dictionary<GraphVertex<T>, VertexState> vertexStates)
        {
            foreach (var neighbor in graphVertex.Neighbours)
            {
                if (vertexStates[neighbor] == VertexState.Visited)
                {
                    continue;
                }
                if (vertexStates[neighbor] == VertexState.Visiting)
                {
                    HasCycle = true;
                    return;
                }
                vertexStates[neighbor] = VertexState.Visiting;

                TopologicalSortHelper(neighbor, stack, vertexStates);

                vertexStates[neighbor] = VertexState.Visited;
            }

            stack.Add(graphVertex);
        }

        public Dictionary<GraphVertex<T>, int> Dijkstra(GraphVertex<T> initialGraphVertex)
        {
            var unvisitedSet = new HashSet<GraphVertex<T>>();
            var visitedSet = new HashSet<GraphVertex<T>>();
            var distancesFromInitialVertex = new Dictionary<GraphVertex<T>, int>();
            InitializeDistances(initialGraphVertex, distancesFromInitialVertex, unvisitedSet);

            while (unvisitedSet.Count != 0)
            {
                var current = ExtractMin(unvisitedSet, distancesFromInitialVertex);
                visitedSet.Add(current);
                foreach (var neighbor in current.Neighbours)
                {
                    Relax(current, neighbor, distancesFromInitialVertex);
                }

            }
            return distancesFromInitialVertex;
        }

        private void Relax(GraphVertex<T> current, GraphVertex<T> neighbour, Dictionary<GraphVertex<T>, int> distancesFromInitialVertex)
        {
            if (distancesFromInitialVertex[current] == Infinity) return;
            var newDistance = distancesFromInitialVertex[current] + current.Costs[neighbour];
            var oldDistance = distancesFromInitialVertex[neighbour];
            if (newDistance < oldDistance)
            {
                distancesFromInitialVertex[neighbour] = newDistance;
            }
        }

        private GraphVertex<T> ExtractMin(HashSet<GraphVertex<T>> unvisitedSet, Dictionary<GraphVertex<T>, int> distancesFromInitialVertex)
        {
            var extractedVertex = unvisitedSet.FirstOrDefault(x => distancesFromInitialVertex[x] == unvisitedSet.Min(y => distancesFromInitialVertex[y]));
            unvisitedSet.Remove(extractedVertex);
            return extractedVertex;
        }

        private void InitializeDistances(GraphVertex<T> initialGraphVertex, Dictionary<GraphVertex<T>, int> distances, HashSet<GraphVertex<T>> unvisitedSet)
        {
            distances.Add(initialGraphVertex, 0);
            foreach (var vertex in Vertices)
            {
                unvisitedSet.Add(vertex);
                if (!vertex.Equals(initialGraphVertex))
                {
                    distances.Add(vertex, Infinity);
                }
            }
        }

        public void BreathFirstTraversal(GraphVertex<T> initialGraphVertex, Action<GraphVertex<T>> action)
        {
            var vertexStates = Vertices.ToDictionary(key => key, value => VertexState.NotVisited);
            var queue = new Queue<GraphVertex<T>>();
            queue.Enqueue(initialGraphVertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                foreach (var neighbor in currentVertex.Neighbours)
                {
                    if (vertexStates[neighbor] != VertexState.Visited)
                    {
                        vertexStates[neighbor] = VertexState.Visited;
                        action(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        public void DepthFirstTraversal(GraphVertex<T> initalGraphVertex, Action<GraphVertex<T>> action)
        {
            if (initalGraphVertex == null) return;
            var vertexStates = Vertices.ToDictionary(key => key, value => VertexState.NotVisited);
            DepthFirstTraversalHelper(initalGraphVertex, vertexStates, action);
        }

        private void DepthFirstTraversalHelper(GraphVertex<T> initalGraphVertex, Dictionary<GraphVertex<T>, VertexState> vertexStates, Action<GraphVertex<T>> action)
        {
            action(initalGraphVertex);
            vertexStates[initalGraphVertex] = VertexState.Visited;

            foreach (var neighbour in initalGraphVertex.Neighbours)
            {
                if (vertexStates[neighbour] == VertexState.NotVisited)
                {
                    DepthFirstTraversalHelper(neighbour, vertexStates, action);
                }
            }
        }
    }
}