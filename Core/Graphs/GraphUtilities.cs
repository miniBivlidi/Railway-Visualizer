using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
    //Usually, when we work with a graph, we use specific classes for Node and Edge,
    //but now, for simplicity and demonstration of the algorithm, we use simple tuples
    public static class GraphUtilities {
        public static Dictionary<T, IList<(T, T)>> BuildGraph<T>(IEnumerable<(T, T)> edges) {
            static void AddAdjacentEdge(T node, (T, T) adjacentEdge, Dictionary<T, IList<(T, T)>> graph) {
                IList<(T, T)> adjacentNodes;
                if(!graph.TryGetValue(node, out adjacentNodes)) {
                    adjacentNodes = new List<(T, T)>();
                    graph[node] = adjacentNodes;
                }
                adjacentNodes.Add(adjacentEdge);
            }
            var graph = new Dictionary<T, IList<(T, T)>>();
            foreach(var edge in edges) {
                AddAdjacentEdge(edge.Item1, edge, graph);
                AddAdjacentEdge(edge.Item2, edge, graph);
            }
            return graph;
        }
    }
}
