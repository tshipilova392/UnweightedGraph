using System;
using System.Collections.Generic;

namespace UnweightedGraph
{
    public static class GraphExtensions
    {
        public static IEnumerable<T> BreadthSearch<T>(this Graph<T> graph, T start)
        {
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            Node startNode = graph.GetNodeFromDictionary(start);
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return graph.GetObjectByNode(node);

                foreach (var incidentEdge in node.outgoingEdges)
                {
                    queue.Enqueue(incidentEdge.To);
                }
            }
        }
    }

    public class Program
    {
        
        static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>();

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(1, 5);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 7);
            graph.AddEdge(5, 1);
            graph.AddEdge(5, 3);
            graph.AddEdge(5, 7);
            graph.AddEdge(6, 4);
            graph.AddEdge(6, 5);
            graph.AddEdge(3, 8);
            graph.AddEdge(7, 8);

            graph.AddNode(9);
            graph.AddNode(10);
            graph.AddEdge(9, 10);

            foreach (var e in graph.BreadthSearch(10))
                Console.WriteLine(e);
        }
    }
}
