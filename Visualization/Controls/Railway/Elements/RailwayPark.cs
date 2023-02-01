using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Visualization {
    public class RailwayPark : RailwayBaseElement {
        public static readonly DependencyProperty PointsProperty;
        public static readonly DependencyProperty LabelProperty;
        public static readonly DependencyProperty LabelColorProperty;
        public static readonly DependencyProperty LabelPositionProperty;
        static readonly DependencyPropertyKey LabelPositionPropertyKey;

        static RailwayPark() {
            var controlType = typeof(RailwayPark);
            DefaultStyleKeyProperty.OverrideMetadata(controlType, new FrameworkPropertyMetadata(controlType));
            PointsProperty = DependencyProperty.Register(nameof(Points), typeof(IEnumerable<Point>), controlType, new FrameworkPropertyMetadata(null));
            LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), controlType, new FrameworkPropertyMetadata(null));
            LabelColorProperty = DependencyProperty.Register(nameof(LabelColor), typeof(Brush), controlType, new FrameworkPropertyMetadata(null));
            LabelPositionPropertyKey = DependencyProperty.RegisterReadOnly(nameof(LabelPosition), typeof(Point), controlType, new FrameworkPropertyMetadata(default(Point)));
            LabelPositionProperty = LabelPositionPropertyKey.DependencyProperty;
        }

        public IEnumerable<Point> Points {
            get { return (IEnumerable<Point>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        public string Label {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public Brush LabelColor {
            get { return (Brush)GetValue(LabelColorProperty); }
            set { SetValue(LabelColorProperty, value); }
        }
        public Point LabelPosition {
            get { return (Point)GetValue(LabelPositionProperty); }
            private set { SetValue(LabelPositionPropertyKey, value); }
        }

        PathGeometry PathGeometry { get { return (PathGeometry)Geometry; } }

        protected override Geometry GetGeometry() {
            return new PathGeometry();
        }
        protected override void UpdateGeometry() {
            PathGeometry.Figures.Clear();
            PathGeometry.Figures.Add(CreateClosedFigure(Points));
            //we should correct this position with label visual container size
            LabelPosition = RectangleHelper.GetPolygonCenter(Points);
        }
        static PathFigure CreateClosedFigure(IEnumerable<Point> points) {
            var figure = new PathFigure();
            figure.IsClosed = true;
            using(var enumerator = points.GetEnumerator()) {
                enumerator.MoveNext();
                figure.StartPoint = enumerator.Current;

                while(enumerator.MoveNext()) {
                    figure.Segments.Add(new LineSegment() { Point = enumerator.Current });
                }
            }
            return figure;
        }
    }
}
