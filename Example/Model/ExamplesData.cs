using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Example {
    public enum StationExampleType {
        SimpleExample,
        TwoSeparatedParks,
        DataBoundPark,
    }
    public static class ExamplesData {
        static readonly RailwayStation simpleExample = new RailwayStation(
            new RailwayPoint[] { 
                new RailwayPoint(new Point(200, 200)),
                new RailwayPoint(new Point(1300, 300)),
            },
            new RailwayLine[] {
                new RailwayLine(new Point(100, 200), new Point(200, 200)),
                new RailwayLine(new Point(200, 200), new Point(300, 300)),
                new RailwayLine(new Point(300, 300), new Point(400, 400)),
                new RailwayLine(new Point(200, 200), new Point(1200, 200)),
                new RailwayLine(new Point(300, 300), new Point(1300, 300)),
                new RailwayLine(new Point(1200, 200), new Point(1300, 300)),
                new RailwayLine(new Point(1400, 400), new Point(1300, 300)),
                new RailwayLine(new Point(1400, 400), new Point(400, 400)),
                new RailwayLine(new Point(1300, 300), new Point(1400, 300)),
            }
        );
        static readonly RailwayStation twoSeparatedParks = new RailwayStation(
            new RailwayPoint[] {
                new RailwayPoint(new Point(200, 200)),
                new RailwayPoint(new Point(1000, 300)),
            },
            new RailwayLine[] {
                new RailwayLine(new Point(100, 100), new Point(200, 200)),
                new RailwayLine(new Point(1000, 100), new Point(100, 100)),
                new RailwayLine(new Point(1100, 200), new Point(1000, 100)),
                new RailwayLine(new Point(200, 200), new Point(1100, 200)),

                new RailwayLine(new Point(100, 300), new Point(200, 400)),
                new RailwayLine(new Point(1000, 300), new Point(100, 300)),
                new RailwayLine(new Point(1100, 400), new Point(1000, 300)),
                new RailwayLine(new Point(200, 400), new Point(1100, 400)),

                new RailwayLine(new Point(200, 200), new Point(1000, 300)),
            }
        );
        static readonly RailwayStation dataBoundPark = new RailwayStation(
          new RailwayPoint[] {
                new RailwayPoint(new Point(1000, 100)),
                new RailwayPoint(new Point(400, 200)),
          },
          new RailwayLine[] {
                new RailwayLine(new Point(300, 100), new Point(400, 200)),
                new RailwayLine(new Point(1000, 100), new Point(300, 100)),
                new RailwayLine(new Point(1100, 200), new Point(1000, 100)),
                new RailwayLine(new Point(400, 200), new Point(1100, 200)),
          },
          new RailwayPark[] { 
              new RailwayPark(
                  new Point[] {
                     new Point(200, 0),
                     new Point(1300, 0),
                     new Point(1400, 200),
                     new Point(1300, 400),
                     new Point(200, 400),
                     new Point(100, 200),
                  }),
          }
      );

        public static RailwayStation GetStationExample(StationExampleType exampleType) {
            switch(exampleType) {
                case StationExampleType.SimpleExample:
                    return simpleExample;
                case StationExampleType.TwoSeparatedParks:
                    return twoSeparatedParks;
                case StationExampleType.DataBoundPark:
                    return dataBoundPark;
            }
            throw new ArgumentException(nameof(StationExampleType));
        }
    }
}
