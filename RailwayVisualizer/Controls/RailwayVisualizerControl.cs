using System;
using System.Windows;
using System.Windows.Controls;

namespace RailwayVisualizer {
    public static class GraphUtilities
    public class RailwayVisualizerControl : ItemsControl {

        static RailwayVisualizerControl() {
            var controlType = typeof(RailwayVisualizerControl);
            DefaultStyleKeyProperty.OverrideMetadata(controlType, new FrameworkPropertyMetadata(controlType));

        }

        public RailwayVisualizerControl() {
            DefaultStyleKey = typeof(RailwayVisualizerControl);
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
        }
    }
}
