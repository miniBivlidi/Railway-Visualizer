using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
    public static class GraphCyclesFinder {
        public static IEnumerable<IEnumerable<T>> FindCycles<T>(IEnumerable<(T, T)> undirectedEdges) {
            if(undirectedEdges == null || !undirectedEdges.Any())
                return Enumerable.Empty<IEnumerable<T>>();
            var cycles = new List<HashSet<T>>();
            var graph = GraphUtilities.BuildGraph(undirectedEdges);
            //last node and previous edge and all path
            var start = graph.First().Key;
            var currentPaths = new Queue<(T, (T, T), HashSet<T>)>();
            currentPaths.Enqueue(ValueTuple.Create(start, default((T, T)), new HashSet<T>() { start }));

            //classic DFS algorithm to reach all nodes from the start node and find all cycles
            while(currentPaths.Count > 0) {
                var size = currentPaths.Count;
                while(--size >= 0) {
                    var currentPathInfo = currentPaths.Dequeue();
                    var adjacentEdges = graph[currentPathInfo.Item1];
                    foreach(var adjacentEdge in adjacentEdges) {
                        if(Equals(adjacentEdge, currentPathInfo.Item2))
                            continue;
                        var oppositeNode = GetOppositeNode(currentPathInfo.Item1, adjacentEdge);
                        if(currentPathInfo.Item3.Contains(oppositeNode)) {
                            var newArea = GetCycle(oppositeNode, currentPathInfo.Item3);
                            if(IsNewCycleFound(cycles, newArea))
                                cycles.Add(newArea);
                        } else {
                            var nextPath = new HashSet<T>(currentPathInfo.Item3);
                            nextPath.Add(oppositeNode);
                            currentPaths.Enqueue(ValueTuple.Create(oppositeNode, adjacentEdge, nextPath));
                        }
                    }
                }
            }
            //we cannot separate or aggregate cycles with abstract data
            //because we know only relations between elements not their real position
            return cycles.ToArray();
        }
        static T GetOppositeNode<T>(T current, (T, T) edge) {
            return Equals(current, edge.Item1) ? edge.Item2 : edge.Item1;
        }
        static HashSet<T> GetCycle<T>(T first, HashSet<T> path) {
            var closedArea = new HashSet<T>();
            foreach(var item in path.SkipWhile(x => !Equals(x, first))) {
                closedArea.Add(item);
            }
            return closedArea;
        }
        static bool IsNewCycleFound<T>(IEnumerable<HashSet<T>> areas, HashSet<T> newArea) {
            foreach(var area in areas) {
                if(newArea.Count != area.Count)
                    continue;
                if(area.SetEquals(newArea))
                    return false;
            }
            return true;
        }
    }
}
