using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Visualization {
    [TemplatePart(Name = PART_ElementsPanel, Type = typeof(Grid))]
    public class RailwayVisualizerControl : Control {
        const string PART_ElementsPanel = "PART_ElementsPanel";

        //we should use DataTemplate or DataTemplateSelector to display each data item with a different visual container,
        //but now we support three visual containers (Line, Point and Park)
        //and have the corresponding properties to retrieve data items, position, color and etc.
        public static readonly DependencyProperty LineSourceProperty;
        public static readonly DependencyProperty LineStartMemberProperty;
        public static readonly DependencyProperty LineEndMemberProperty;
        public static readonly DependencyProperty LineColorProperty;
        public static readonly DependencyProperty LinesProperty;
        static readonly DependencyPropertyKey LinesPropertyKey;

        public static readonly DependencyProperty ParkSourceProperty;
        public static readonly DependencyProperty ParkDataMemberProperty;
        public static readonly DependencyProperty ParkColorProperty;
        public static readonly DependencyProperty ParksProperty;
        static readonly DependencyPropertyKey ParksPropertyKey;

        public static readonly DependencyProperty PointSourceProperty;
        public static readonly DependencyProperty PointPositionMemberProperty;
        public static readonly DependencyProperty PointColorProperty;
        public static readonly DependencyProperty PointsProperty;
        static readonly DependencyPropertyKey PointsPropertyKey;

        static RailwayVisualizerControl() {
            var controlType = typeof(RailwayVisualizerControl);
            DefaultStyleKeyProperty.OverrideMetadata(controlType, new FrameworkPropertyMetadata(controlType));
            LineSourceProperty = DependencyProperty.Register(nameof(LineSource), typeof(object), controlType, new FrameworkPropertyMetadata(null, TryGenerateLines));
            LineStartMemberProperty = DependencyProperty.Register(nameof(LineStartMember), typeof(string), controlType, new FrameworkPropertyMetadata(null, TryGenerateLines));
            LineEndMemberProperty = DependencyProperty.Register(nameof(LineEndMember), typeof(string), controlType, new FrameworkPropertyMetadata(null, TryGenerateLines));
            LineColorProperty = DependencyProperty.Register(nameof(LineColor), typeof(Brush), controlType, new FrameworkPropertyMetadata(null, UpdateLineColor));
            LinesPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Lines), typeof(ReadOnlyCollection<RailwayLine>), controlType, new FrameworkPropertyMetadata(null));
            LinesProperty = LinesPropertyKey.DependencyProperty;
       
            ParkSourceProperty = DependencyProperty.Register(nameof(ParkSource), typeof(object), controlType, new FrameworkPropertyMetadata(null, TryGenerateParks));
            ParkDataMemberProperty = DependencyProperty.Register(nameof(ParkDataMember), typeof(string), controlType, new FrameworkPropertyMetadata(null, TryGenerateParks));
            ParkColorProperty = DependencyProperty.Register(nameof(ParkColor), typeof(Brush), controlType, new FrameworkPropertyMetadata(null, UpdateParksColor));
            ParksPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Parks), typeof(ReadOnlyCollection<RailwayPark>), controlType, new FrameworkPropertyMetadata(null));
            ParksProperty = ParksPropertyKey.DependencyProperty;

            PointSourceProperty = DependencyProperty.Register(nameof(PointSource), typeof(object), controlType, new FrameworkPropertyMetadata(null, TryGeneratePoints));
            PointPositionMemberProperty = DependencyProperty.Register(nameof(PointPositionMember), typeof(string), controlType, new FrameworkPropertyMetadata(null, TryGeneratePoints));
            PointColorProperty = DependencyProperty.Register(nameof(PointColor), typeof(Brush), controlType, new FrameworkPropertyMetadata(null, UpdatePointsColor));
            PointsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Points), typeof(ReadOnlyCollection<RailwayPoint>), controlType, new FrameworkPropertyMetadata(null));
            PointsProperty = PointsPropertyKey.DependencyProperty;
        }

        public object LineSource {
            get { return GetValue(LineSourceProperty); }
            set { SetValue(LineSourceProperty, value); }
        }
        public string LineStartMember {
            get { return (string)GetValue(LineStartMemberProperty); }
            set { SetValue(LineStartMemberProperty, value); }
        }
        public string LineEndMember {
            get { return (string)GetValue(LineEndMemberProperty); }
            set { SetValue(LineEndMemberProperty, value); }
        }
        public Brush LineColor {
            get { return (Brush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        public ReadOnlyCollection<RailwayLine> Lines {
            get { return (ReadOnlyCollection<RailwayLine>)GetValue(LinesProperty); }
            private set { SetValue(LinesPropertyKey, value); }
        }
        static void TryGenerateLines(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayVisualizerControl)sender).TryGenerateLines();
        }
        static void UpdateLineColor(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayVisualizerControl)sender).UpdateLineColor();
        }

        public object ParkSource {
            get { return GetValue(ParkSourceProperty); }
            set { SetValue(ParkSourceProperty, value); }
        }
        public string ParkDataMember {
            get { return (string)GetValue(ParkDataMemberProperty); }
            set { SetValue(ParkDataMemberProperty, value); }
        }
        public Brush ParkColor {
            get { return (Brush)GetValue(ParkColorProperty); }
            set { SetValue(ParkColorProperty, value); }
        }
        public ReadOnlyCollection<RailwayPark> Parks {
            get { return (ReadOnlyCollection<RailwayPark>)GetValue(ParksProperty); }
            private set { SetValue(ParksPropertyKey, value); }
        }
        static void TryGenerateParks(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayVisualizerControl)sender).TryGenerateParks();
        }
        static void UpdateParksColor(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayVisualizerControl)sender).UpdateParksColor();
        }

        public object PointSource {
            get { return GetValue(PointSourceProperty); }
            set { SetValue(PointSourceProperty, value); }
        }
        public string PointPositionMember {
            get { return (string)GetValue(PointPositionMemberProperty); }
            set { SetValue(PointPositionMemberProperty, value); }
        }
        public Brush PointColor {
            get { return (Brush)GetValue(PointColorProperty); }
            set { SetValue(PointColorProperty, value); }
        }
        public ReadOnlyCollection<RailwayPoint> Points {
            get { return (ReadOnlyCollection<RailwayPoint>)GetValue(PointsProperty); }
            private set { SetValue(PointsPropertyKey, value); }
        }
        static void TryGeneratePoints(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayVisualizerControl)sender).TryGeneratePoints();
        }
        static void UpdatePointsColor(object sender, DependencyPropertyChangedEventArgs args) {
            ((RailwayVisualizerControl)sender).UpdatePointsColor();
        }

        bool LinePropertiesReady { get { return LineSource != null && !string.IsNullOrEmpty(LineStartMember) && !string.IsNullOrEmpty(LineEndMember); } }
        List<RailwayLine> LinesCore { get; }
        PropertyDescriptor LineStartDataProperty { get; set; }
        PropertyDescriptor LineEndDataProperty { get; set; }

        bool ParkPropertiesReady { get { return ParkSource != null && !string.IsNullOrEmpty(ParkDataMember); } }
        List<RailwayPark> ParksCore { get; }
        PropertyDescriptor ParkDataProperty { get; set; }
        string ParkDataMemberActualMember { get { return ParkPropertiesReady ? ParkDataMember : string.Empty; } }
        string ParkLabel { get { return ParkPropertiesReady ? "Data Park Label" : "Generated Park Label"; } }

        bool PointPropertiesReady { get { return PointSource != null && !string.IsNullOrEmpty(PointPositionMember); } }
        List<RailwayPoint> PointsCore { get; }
        PropertyDescriptor PointPositionProperty { get; set; }

        Grid ElementPanel { get; set; }

        public RailwayVisualizerControl() {
            LinesCore = new List<RailwayLine>();
            Lines = new ReadOnlyCollection<RailwayLine>(LinesCore);

            ParksCore = new List<RailwayPark>();
            Parks = new ReadOnlyCollection<RailwayPark>(ParksCore);
            
            PointsCore = new List<RailwayPoint>();
            Points = new ReadOnlyCollection<RailwayPoint>(PointsCore);
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            ElementPanel = (Grid)GetTemplateChild(PART_ElementsPanel);
            TryGenerateLines();
            TryGenerateParks();
            TryGeneratePoints();
        }

        void TryGenerateLines() {
            TryGenerateVisualElements(LinePropertiesReady, GenerateLines);
            //we should check if Parks source are bound,
            //but to keep things simple,
            //we only check the values of the properties
            TryGenerateVisualElements(!ParkPropertiesReady, GenerateParksViaLines);
        }
        void TryGenerateParks() {
            //generate from source if the corresponding property is set
            //or from lines if the corresponding property was reset
            TryGenerateVisualElements(ParkPropertiesReady, GenerateParks);
            TryGenerateVisualElements(!ParkPropertiesReady, GenerateParksViaLines);
        }
        void TryGeneratePoints() {
            TryGenerateVisualElements(PointPropertiesReady, GeneratePoints);
        }
        void TryGenerateVisualElements(bool propertiesReady, Action generate) {
            //we should regenerate lines after changing any property,
            //but to keep things simple,
            //we only generate string once when all properties have been set.
            if(propertiesReady && ElementPanel != null)
                generate();
        }

        void GenerateLines() {
            GenerateVisualElement(LineSource, CreateLine, LinesCore);
        }
        void GenerateParks() {
            GenerateParksCore(ParkSource);
        }
        void GenerateParksViaLines() {
            //we should create a property to determine behavior with nested cycles (split into smaller or aggregate into larger)
            //but to keep things simple,
            //we aggregate small groups into larger by their bounded rect
            GenerateParksCore(RectangleHelper.AggregateCyclesPolygons(GraphCyclesFinder.FindCycles(Lines.Select(line => (line.Start, line.End)))));
        }
        void GenerateParksCore(object parkSource) {
            GenerateVisualElement(parkSource, CreatePark, ParksCore);
        }
        void GeneratePoints() {
            GenerateVisualElement(PointSource, CreatePoint, PointsCore);
        }
        void GenerateVisualElement<T>(object source, Func<object, T> createVisual, IList<T> collection) where T : RailwayBaseElement  {
            collection.Clear();
            for(int i = ElementPanel.Children.Count -1; i >= 0; i--) {
                var visual = ElementPanel.Children[i];
                if(visual is T)
                    ElementPanel.Children.RemoveAt(i);
            }
            //we should work with different types of collections and correctly respond to changes of properties of a collection or an element,
            //but to keep things simple,
            //we generate a simple enumeration of elements without additional logic and put them directly into the elements panel.

            //we should have a flexible way to organize ZOrder of the element
            //but to keep things simple,
            //we place element in the predefined order - Parks, Lines, Points.
            var visualIndex = GeVisualElementsStartIndex(ElementPanel, typeof(T));
            foreach(var visual in ((IEnumerable<object>)source).Select(createVisual)) {
                collection.Add(visual);
                ElementPanel.Children.Insert(visualIndex, visual);
            }
        }
        static int GeVisualElementsStartIndex(Grid panel, Type visualElementType) {
            if(visualElementType == typeof(RailwayPark))
                return 0;
            if(visualElementType == typeof(RailwayPoint))
                return panel.Children.Count;
            int i = 0;
            for(; i < panel.Children.Count; i++) {
                if(!(panel.Children[i] is RailwayPark))
                    break;
            }
            return i;
        }
        RailwayLine CreateLine(object lineData) {
            return new RailwayLine() {
                DataContext = lineData,
                Start = GetMemberValue<Point>(lineData, LineStartDataProperty, LineStartMember),
                End = GetMemberValue<Point>(lineData, LineEndDataProperty, LineEndMember),
                Color = LineColor
            };
        }
        RailwayPark CreatePark(object parkData) {
            return new RailwayPark() {
                DataContext = parkData,
                Points = GetMemberValue<IEnumerable<Point>>(parkData, ParkDataProperty, ParkDataMemberActualMember),
                Color = ParkColor,
                Label = ParkLabel,
                LabelColor = LineColor
            };
        }
        RailwayPoint CreatePoint(object pointData) {
            return new RailwayPoint() {
                DataContext = pointData,
                Position = GetMemberValue<Point>(pointData, PointPositionProperty, PointPositionMember),
                Color = PointColor
            };
        }
        static T GetMemberValue<T>(object data, PropertyDescriptor property, string member) {
            if(string.IsNullOrEmpty(member))
                return (T)data;
            if(property == null)
                property = ReflectionHelper.GetProperty(data, member);
            return (T)property.GetValue(data);
        }

        void UpdateLineColor() {
            UpdateVisualElementColor(Lines, LineColor);
        }
        void UpdateParksColor() {
            UpdateVisualElementColor(Parks, ParkColor);
        }
        void UpdatePointsColor() {
            UpdateVisualElementColor(Points, PointColor);
        }
        void UpdateVisualElementColor(IEnumerable<RailwayBaseElement> elements, Brush color) {
            foreach(var element in elements) {
                element.Color = color;
            }
        }
    }
}
