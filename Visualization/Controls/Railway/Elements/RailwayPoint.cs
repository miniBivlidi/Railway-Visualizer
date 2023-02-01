using System;
using System.Windows;
using System.Windows.Media;

namespace Visualization {
    public class RailwayPoint : RailwayBaseElement {
        public static readonly DependencyProperty PositionProperty;
        public static readonly DependencyProperty SizeProperty;

        static RailwayPoint() {
            var controlType = typeof(RailwayPoint);
            PositionProperty = DependencyProperty.Register(nameof(Position), typeof(Point), controlType, new FrameworkPropertyMetadata(default(Point), OnGeometryPropertyChanged));
            SizeProperty = DependencyProperty.Register(nameof(Size), typeof(Size), controlType, new FrameworkPropertyMetadata(new Size(6,6), OnGeometryPropertyChanged));
        }

        public Point Position {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        public Size Size {
            get { return (Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        EllipseGeometry EllipseGeometry { get { return (EllipseGeometry)Geometry; } }

        protected override Geometry GetGeometry() {
            return new EllipseGeometry();
        }
        protected override void UpdateGeometry() {
            EllipseGeometry.Center = Position;
            EllipseGeometry.RadiusX = Size.Width;
            EllipseGeometry.RadiusY = Size.Height;
        }
    }
}
