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

        public static List<T> FindPath<T>(this Graph<T> graph, T start, T end)
        {
            var track = new Dictionary<Node, Node>();
            var startNode = graph.GetNodeFromDictionary(start);
            var endNode = graph.GetNodeFromDictionary(end);
            track[startNode] = null;
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                foreach (var incidentEdge in node.outgoingEdges)
                {
                    if (track.ContainsKey(incidentEdge.To)) continue;
                    track[incidentEdge.To] = node;
                    queue.Enqueue(incidentEdge.To);
                }
                if (track.ContainsKey(endNode)) break;
            }
            var pathItem = endNode;
            var result = new List<T>();
            while (pathItem != null)
            {
                result.Add(graph.GetObjectByNode(pathItem));
                pathItem = track[pathItem];
            }
            result.Reverse();
            return result;
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

           // foreach (var e in graph.BreadthSearch(10))
            //    Console.WriteLine(e);

            foreach (var e in graph.FindPath(1,8))
                Console.WriteLine(e);
        }
    }
}
