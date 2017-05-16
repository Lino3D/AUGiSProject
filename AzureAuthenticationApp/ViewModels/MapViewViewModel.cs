using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Acr.UserDialogs;
using AzureAuthenticationApp.Helpers;
using AzureAuthenticationApp.Models;
using Xamarin.Forms;
using Plugin.Geolocator;
namespace AzureAuthenticationApp.ViewModels
{
    class MapViewViewModel : INotifyPropertyChanged
    {
        TodoItemManager<PositionInfo> manager;
        private int _floorNumber = 1;
        protected IUserDialogs Dialogs { get; }
        public ObservableCollection<int> FloorsList { get; }
       


        public MapViewViewModel(IUserDialogs dialogs)
        {
            Dialogs = dialogs;
            manager = TodoItemManager<PositionInfo>.DefaultManager;
            FloorsList = new ObservableCollection<int>();
            for (var i = 0; i < 6; i++)
            {
                FloorsList.Add(i);
            }
            GetLocationCommand = new Command(GetLocation);
            //var positionList = manager.GetItemsAsync();
        }

        public int FloorNumber
        {
            get => _floorNumber;
            set {
                _floorNumber = value;
                OnPropertyChanged(); }
        }




        public ICommand GetLocationCommand { get; }

        public async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(1000);
            if (position == null) return;
            var lat = position.Latitude;
            var lon = position.Latitude;
            var message = string.Format("Lat: {0}, Lon {1}", lat, lon);
            Dialogs.Alert(message, "Your Position, adding to azure.");

            var pos = new PositionInfo(lon,lat,0,"0");
            await manager.SaveAsync(pos);
        }





        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
