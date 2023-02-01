using NUnit.Framework;
using System;
using System.Windows;
using Visualization;

namespace Tests {
    [TestFixture]
    public class RailwayVisualizerControlTests {
        Window Window { get; set; } 
        RailwayVisualizerControl Control { get; set; }

        [SetUp]
        public void SetUp() {
            Control = new RailwayVisualizerControl();
            Window = new Window();
            Window.Content = Control;
            Window.Show();
        }
        [TearDown]
        public void TearDown() {
            Window.Close();
            Window.Content = null;
            Window = null;
            Control = null;
        }

        [Test]
        public void VisualElementCount() {
            Assert.Fail("Test non complete");
        }
        [Test]
        public void LinePositions() {
            Assert.Fail("Test non complete");
        }
        [Test]
        public void ParkPositions() {
            //generate or set via property
            Assert.Fail("Test non complete");
        }
        [Test]
        public void PointPositions() {
            Assert.Fail("Test non complete");
        }
        [Test]
        public void VisualElementGeometry() {
            Assert.Fail("Test non complete");
        }
        [Test]
        public void VisualElementZOrder() {
            Assert.Fail("Test non complete");
        }
        //we also should check other various scenarios - color, element positions on canvas, property changing, regenerate data and etc.
    }
}
