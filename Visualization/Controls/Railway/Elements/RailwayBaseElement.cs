using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Visualization {
    public abstract class RailwayBaseElement : Control {
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty GeometryProperty;
        static readonly DependencyPropertyKey GeometryPropertyKey;

        static RailwayBaseElement() {
            var controlType = typeof(RailwayBaseElement);
            DefaultStyleKeyProperty.OverrideMetadata(controlType, new FrameworkPropertyMetadata(controlType));
            ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Brush), controlType, new FrameworkPropertyMetadata(null, OnGeometryPropertyChanged));
            GeometryPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Geometry), typeof(Geometry), controlType, new FrameworkPropertyMetadata(null));
            GeometryProperty = GeometryPropertyKey.DependencyProperty;
        }

        public Brush Color {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public Geometry Geometry {
            get { return (Geometry)GetValue(GeometryProperty); }
            private set { SetValue(GeometryPropertyKey, value); }
        }

        protected static void OnGeometryPropertyChanged(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayBaseElement)sender).UpdateGeometry();
        }

        public RailwayBaseElement() {
            Geometry = GetGeometry();
        }

        protected abstract Geometry GetGeometry();
        protected abstract void UpdateGeometry();
    }
}
