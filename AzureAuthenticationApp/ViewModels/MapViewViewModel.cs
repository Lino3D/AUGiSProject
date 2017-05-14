using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureAuthenticationApp.ViewModels
{
    class MapViewViewModel : INotifyPropertyChanged
    {

        private int _floorNumber = 1;

        public ObservableCollection<int> FloorsList { get; }


        public MapViewViewModel()
        {
            FloorsList = new ObservableCollection<int>();
            for (var i = 0; i < 6; i++)
            {
                FloorsList.Add(i);
            }
        }

        public int FloorNumber
        {
            get { return _floorNumber; }
            set {
                _floorNumber = value;
                OnPropertyChanged(); }
        }

  



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
