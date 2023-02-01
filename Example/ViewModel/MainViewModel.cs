using System;
using System.ComponentModel;
using System.Linq;

namespace Example {
    public class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        StationExampleType exampleTypeCore;
        public StationExampleType ExampleType { 
            get { return exampleTypeCore; }
            set {
                if(exampleTypeCore == value)
                    return;
                exampleTypeCore = value;
                SyncCurrentExample();
            }
        }

        public RailwayStation CurrentStationExample { get; private set; }

        public MainViewModel() {
            ExampleType = StationExampleType.SimpleExample;
            SyncCurrentExample();
        }

        void SyncCurrentExample() {
            CurrentStationExample = ExamplesData.GetStationExample(exampleTypeCore);
            RaisePropertyChanged(nameof(CurrentStationExample));
        }
        void RaisePropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
