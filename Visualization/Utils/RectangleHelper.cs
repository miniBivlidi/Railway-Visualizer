using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Visualization {
    public static class RectangleHelper {
        public static IEnumerable<Point>[] AggregateCyclesPolygons(IEnumerable<IEnumerable<Point>> cyclesPolygons) {
            if(cyclesPolygons == null || !cyclesPolygons.Any())
                return Enumerable.Empty<IEnumerable<Point>>().ToArray();
            var result = new List<IEnumerable<Point>>();
            var nonAnalzedPoligons = cyclesPolygons.Select(polygon => (polygon, GetBoundedRect(polygon))).ToList();
            //we know that the cycles collection contains all cycles and we finally aggregate them into large one
            while(nonAnalzedPoligons.Count > 0) {
                var otherPolygons = new List<(IEnumerable<Point>, Rect)>();
                var current = nonAnalzedPoligons[0];
                //choose the largest cycle
                for(int i = 1; i < nonAnalzedPoligons.Count; i++) {
                    var nonAnalzedPoligon = nonAnalzedPoligons[i];
                    if(nonAnalzedPoligon.Item2.Contains(current.Item2)) {
                        current = nonAnalzedPoligon;
                    }
                }
                //find non intersected cycles
                for(int i = 0; i < nonAnalzedPoligons.Count; i++) {
                    var nonAnalzedPoligon = nonAnalzedPoligons[i];
                    if(!current.Item2.IntersectsWith(nonAnalzedPoligon.Item2)) {
                        otherPolygons.Add(nonAnalzedPoligon);
                    }
                }
                result.Add(current.Item1);
                nonAnalzedPoligons = otherPolygons;
            }
            return result.ToArray();
        }

        public static Point GetPolygonCenter(IEnumerable<Point> polygon) {
            var boundedRect = GetBoundedRect(polygon);
            return new Point(boundedRect.Left + boundedRect.Width / 2, boundedRect.Top + boundedRect.Height / 2);
        }
        
        static Rect GetBoundedRect(IEnumerable<Point> polygon) {
            return polygon.Aggregate(Rect.Empty, Rect.Union);
        }
    }
}
