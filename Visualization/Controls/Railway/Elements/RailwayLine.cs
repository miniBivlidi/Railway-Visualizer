using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Visualization {
    public class RailwayLine : RailwayBaseElement {
        public static readonly DependencyProperty StartProperty;
        public static readonly DependencyProperty EndProperty;

        static RailwayLine() {
            var controlType = typeof(RailwayLine);
            StartProperty = DependencyProperty.Register(nameof(Start), typeof(Point), controlType, new FrameworkPropertyMetadata(default(Point), OnGeometryPropertyChanged));
            EndProperty = DependencyProperty.Register(nameof(End), typeof(Point), controlType, new FrameworkPropertyMetadata(default(Point), OnGeometryPropertyChanged));
        }

        public Point Start {
            get { return (Point)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        public Point End {
            get { return (Point)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        LineGeometry LineGeometry { get { return (LineGeometry)Geometry; } }

        protected override Geometry GetGeometry() {
            return new LineGeometry();
        }
        protected override void UpdateGeometry() {
            LineGeometry.StartPoint = Start;
            LineGeometry.EndPoint = End;
        }
    }
}
