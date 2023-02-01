using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Example {
    //in this example we do not support changing any properties, but we must implement INPC to avoid memory leak.
    public class RailwayObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class RailwayPoint : RailwayObject {
        public Point Position { get; }

        public RailwayPoint(Point position) {
            Position = position;
        }
    }
    public class RailwayLine : RailwayObject {
        public Point Start { get; }
        public Point End { get; }
        
        public RailwayLine(Point start, Point end) {
            Start = start;
            End = end;
        }
    }
    public class RailwayPark : RailwayObject {
        public Point[] Points { get; }

        public RailwayPark(Point[] points) {
            Points = points;
        }
    }
    public class RailwayStation : RailwayObject {
        public RailwayPoint[] Points { get; }
        public RailwayLine[] Lines { get; }
        public RailwayPark[] Parks { get; }

        public RailwayStation(RailwayPoint[] points, RailwayLine[] lines, RailwayPark[] parks = null) {
            Points = points;
            Lines = lines;
            Parks = parks;
        }
    }
}
