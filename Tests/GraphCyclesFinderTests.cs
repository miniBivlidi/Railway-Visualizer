using NUnit.Framework;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    [TestFixture]
    public class GraphCyclesFinderTests {
        static TestCaseData[] inputGrapths = new TestCaseData[]  {
            Null(),
            EmptyGraph(),
            DirectEdgesGraph(),
            OneCycle(),
            OneCycleWithAdditionalEdges(),
            SeveralConnectedCycles(),
            ConnectedCyclesWithOneCommonNode(),
            DisconnectedGrpahsWithCycles(),
        };
        static TestCaseData Null() {
            return new TestCaseData(
                null,
                Enumerable.Empty<IEnumerable<int>>().ToArray()
            );
        }
        static TestCaseData EmptyGraph() {
            return new TestCaseData(
                Enumerable.Empty<(int, int)>(),
                Enumerable.Empty<IEnumerable<int>>().ToArray()
            );
        }
        static TestCaseData DirectEdgesGraph() {
            return new TestCaseData(
                new (int, int)[] {
                    (0, 1),
                    (1, 2),
                    (2, 3),
                },
                Enumerable.Empty<IEnumerable<int>>().ToArray()
            );
        }
        static TestCaseData OneCycle() {
            return new TestCaseData(
                new (int, int)[] {
                    (3, 1),
                    (2, 3),
                    (1, 2),
                },
                new IEnumerable<int>[] { 
                    new [] { 1, 2, 3 },
                }
            );
        }
        static TestCaseData OneCycleWithAdditionalEdges() {
            return new TestCaseData(
                new (int, int)[] {
                    (3, 1),
                    (0, 1),
                    (1, 2),
                    (4, 3),
                    (5, 2),
                    (2, 3),
                },
                new IEnumerable<int>[] {
                    new [] { 1, 2, 3 },
                }
            );
        }
        static TestCaseData SeveralConnectedCycles() {
            return new TestCaseData(
                new (int, int)[] {
                    (2, 3),
                    (0, 1),
                    (4, 5),
                    (5, 1),
                    (3, 4),
                    (1, 2),
                    (4, 0),
                },
                new IEnumerable<int>[] {
                    new [] { 2, 3, 4, 5, 1 },
                    new [] { 2, 3, 4, 0, 1 },
                    new [] { 4, 0, 1, 5 },
                }
            );
        }
        static TestCaseData ConnectedCyclesWithOneCommonNode() {
            return new TestCaseData(
               new (int, int)[] {
                    (4, 1),
                    (2, 0),
                    (0, 1),
                    (1, 3),
                    (3, 4),
                    (1, 2),
                },
                new IEnumerable<int>[] {
                    new [] { 0, 1, 2 },
                    new [] { 1, 3, 4 },
                }
            );
        }
        static TestCaseData DisconnectedGrpahsWithCycles() {
            return new TestCaseData(
                new (int, int)[] {
                    (2, 0),
                    (0, 1),
                    (3, 4),
                    (5, 3),
                    (1, 2),
                    (4, 5),
                },
                new IEnumerable<int>[] {
                    new [] { 0, 1, 2 },
                    new [] { 5, 3, 4 },
                }
            );
        }

        [TestCaseSource(nameof(inputGrapths))]
        public void CheckGraphCyclesFinder(IEnumerable<(int, int)> graph, IEnumerable<int>[] expectedCycles) {
            var result = GraphCyclesFinder.FindCycles(graph).ToArray();
            TestUtils.AreCollectionsEquivalent(result, expectedCycles);
        }
    }
}
