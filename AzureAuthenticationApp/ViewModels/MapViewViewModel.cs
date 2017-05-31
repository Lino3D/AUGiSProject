using Acr.UserDialogs;
using AzureAuthenticationApp.Helpers;
using AzureAuthenticationApp.Models;
using AzureAuthenticationApp.Models.Interfaces;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.WifiInfo;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

//using Plugin.Geolocator;
namespace AzureAuthenticationApp.ViewModels
{
    public class MapViewViewModel : INotifyPropertyChanged
    {

        private int _floorNumber = 1;
        private ConnectionHandler _connectionHandler;
        protected IUserDialogs Dialogs { get; }
        public ObservableCollection<int> FloorsList { get; }
        public ConnectionHandler ConnectionHandler
        {
            get => _connectionHandler;
            set
            {
                _connectionHandler = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<IMapModel> _usersPins = new ObservableCollection<IMapModel>();

        public BlobContainerManager BlobManager;
        private IGeolocator _mainGeolocator;
        private UserPin myPin;


        public MapViewViewModel(IUserDialogs dialogs /*, TodoItemManager<PositionInfo> manager*/)
        {
            myPin = new UserPin()
            {
                Name = "Mainuser"
            };
            ;
            InitializeConnectionHandlers();
            BlobManager = new BlobContainerManager("Mainuser");
            Dialogs = dialogs;
            FloorsList = new ObservableCollection<int>();
            for (var i = 0; i < 6; i++)
            {
                FloorsList.Add(i);
            }
            GetLocationCommand = new Command(ManualLocation);


        }



        private void InitializeConnectionHandlers()
        {
            ConnectionHandler = new ConnectionHandler();
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                ConnectionHandler.Connected = CrossConnectivity.Current.IsConnected;
                ConnectionHandler.CurrectConnectionTypes = CrossConnectivity.Current.ConnectionTypes.ToList();

            };

            CrossConnectivity.Current.ConnectivityTypeChanged += async (sender, args) =>
            {
                ConnectionHandler.Connected = CrossConnectivity.Current.IsConnected;
                ConnectionHandler.CurrectConnectionTypes = CrossConnectivity.Current.ConnectionTypes.ToList();
                ConnectionHandler.CalculateStrenghts();
                if (ConnectionHandler.WifiBssid == null || !ConnectionHandler.Connected) return;
                var lol = await BlobManager.PerformPositionRequest(ConnectionHandler.WifiStrenght, ConnectionHandler.WifiBssid);
                if (!string.IsNullOrEmpty(lol?.ToString()))
                    GetLocation(lol.ToString());
            };
            CrossWifiInfo.Current.SignalStrengthChanged += async (sender, args) =>
            {
                ConnectionHandler.CalculateStrenghts();
                if (ConnectionHandler.WifiBssid == null || !ConnectionHandler.Connected) return;
                var lol = await BlobManager.PerformPositionRequest(ConnectionHandler.WifiStrenght, ConnectionHandler.WifiBssid);
                if (!string.IsNullOrEmpty(lol?.ToString()))
                    GetLocation(lol.ToString());
            };
        }

        public int FloorNumber
        {
            get => _floorNumber;
            set
            {
                _floorNumber = value;
                OnPropertyChanged();
            }
        }




        public ICommand GetLocationCommand { get; }

        public async void GetLocation(string data)
        {
            var position = new Position();
            try
            {
                _mainGeolocator = CrossGeolocator.Current;
                _mainGeolocator.DesiredAccuracy = 5;
                position = await _mainGeolocator.GetPositionAsync(5000);
            }
            catch (Exception ex)
            {
                Dialogs.Alert("Unable to get location, may need to increase timeout: " + ex);
            }
            if (position == null) return;
            myPin.Location = new Location { Latitude = position.Latitude, Longitude = position.Longitude };
            myPin.Details = data;
            ClearOldPins();
            UserPins.Add(myPin);
        }

        private void ClearOldPins()
        {
            var oldPin = UserPins.FirstOrDefault(x => x.Name == "Mainuser");
            if (oldPin != null)
            {
                UserPins.Remove(oldPin);
            }
        }

        private async void ManualLocation()
        {
            ConnectionHandler.Connected = CrossConnectivity.Current.IsConnected;
            ConnectionHandler.CurrectConnectionTypes = CrossConnectivity.Current.ConnectionTypes.ToList();
            ConnectionHandler.CalculateStrenghts();
            if (ConnectionHandler.WifiBssid == null || !ConnectionHandler.Connected) return;
            var lol = await BlobManager.PerformPositionRequest(ConnectionHandler.WifiStrenght, ConnectionHandler.WifiBssid);
            if (!string.IsNullOrEmpty(lol))
                GetLocation(lol);
        }



        public ObservableCollection<IMapModel> UserPins
        {
            get => _usersPins;
            set
            {
                _usersPins = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserPins"));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
