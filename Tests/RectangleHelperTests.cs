using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Visualization;

namespace Tests {
    [TestFixture]
    public class RectangleHelperTests {
        static TestCaseData[] inputGrapths = new TestCaseData[]  {
            Null(),
            EmptyGraph(),
            SeparatedPolygons(),
            PolygonGroups(),
        };
        static TestCaseData Null() {
            return new TestCaseData(
                null,
                Enumerable.Empty<IEnumerable<Point>>().ToArray()
            );
        }
        static TestCaseData EmptyGraph() {
            return new TestCaseData(
                Enumerable.Empty<IEnumerable<Point>>(),
                Enumerable.Empty<IEnumerable<Point>>().ToArray()
            );
        }
        static TestCaseData SeparatedPolygons() {
            return new TestCaseData(
                new Point[][] {
                    new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1) },
                    new Point[] { new Point(0, 3), new Point(1, 3), new Point(1, 4), new Point(0, 4) },
                },
                new Point[][] {
                    new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1) },
                    new Point[] { new Point(0, 3), new Point(1, 3), new Point(1, 4), new Point(0, 4) },
                }
            );
        }
        static TestCaseData PolygonGroups() {
            return new TestCaseData(
                new Point[][] {
                    new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1) },
                    new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 2), new Point(0, 2) },
                    new Point[] { new Point(0, 2), new Point(1, 2), new Point(1, 3), new Point(0, 3) },
                    new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 3), new Point(0, 3) },
                },
                new Point[][] {
                    new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 3), new Point(0, 3) },
                }
            );
        }
       

        [TestCaseSource(nameof(inputGrapths))]
        public void AggregatePolygonsTest(IEnumerable<IEnumerable<Point>> polygons, IEnumerable<Point>[] expectedPolygons) {
            var result = RectangleHelper.AggregateCyclesPolygons(polygons);
            TestUtils.AreCollectionsEquivalent(result, expectedPolygons);
        }
    }
}
