using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphTheory
{
    public static class PrintHelpers
    {
        public static void Print(int[][] matrix)
        {
            for (int i = 0; i < matrix[0].Length; i++)
            {
                for (int j = 0; j < matrix[0].Length - 1; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.WriteLine(matrix[i][matrix[0].Length - 1]);
            }
        }

        public static void PrintReversed<T>(IEnumerable<T> enumerable, string delim)
        {
            var array = enumerable.ToArray();

            for (int i = array.Length - 1; i > 0; i--)
            {
                Console.Write(array[i] + delim);
            }
            Console.WriteLine(array[0]);
        }

        public static void Print<T>(IEnumerable<T> enumerable, string delim)
        {
            var array = enumerable.ToArray();
            if (array.Length == 0)
                return;
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + delim);
            }
            Console.WriteLine(array[array.Length - 1]);
        }

        public static void Print<T>(List<Edge<T>> edges) where T : class, IComparable
        {
            foreach (var edge in edges)
            {
                edge.Display();
            }
        }

        public static void PrintShortestPaths<T>(Graph<T> graph) where T : class, IComparable
        {
            foreach (var vertex in graph.Vertices)
            {
                var shortestPath = graph.Dijkstra(vertex);
                foreach (var dist in shortestPath)
                {
                    Console.WriteLine($"[{vertex},{dist.Key}] = {dist.Value}");
                }
                Console.WriteLine();
            }
        }
    }
}
