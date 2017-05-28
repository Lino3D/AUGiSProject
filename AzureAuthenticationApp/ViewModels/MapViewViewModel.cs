using Acr.UserDialogs;
using AzureAuthenticationApp.Helpers;
using Plugin.Connectivity;
using Plugin.WifiInfo;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
//using Plugin.Geolocator;
namespace AzureAuthenticationApp.ViewModels
{
    //DefaultEndpointsProtocol=https;AccountName=gisappfiles;AccountKey=yHxXdjYFoQdIPyJabvR+jd3xDHxm5UBDiTay6gqmJhO38pmnCdKMMKlrMy9tSdVcQQRyrkIWYDcv3CJMrbeVyA==;EndpointSuffix=core.windows.net
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
        //  private TodoItemManager<PositionInfo> manager;


        public MapViewViewModel(IUserDialogs dialogs /*, TodoItemManager<PositionInfo> manager*/)
        {
            InitializeConnectionHandlers();

            Dialogs = dialogs;
            // this.manager = manager;
            FloorsList = new ObservableCollection<int>();
            for (var i = 0; i < 6; i++)
            {
                FloorsList.Add(i);
            }
            GetLocationCommand = new Command(GetLocation);
            //var positionList = manager.GetItemsAsync();
        }

        private void InitializeConnectionHandlers()
        {
            ConnectionHandler = new ConnectionHandler();
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                ConnectionHandler.Connected = CrossConnectivity.Current.IsConnected;
                ConnectionHandler.CalculateStrenghts();
            };

            CrossConnectivity.Current.ConnectivityTypeChanged += (sender, args) =>
            {
                ConnectionHandler.CurrectConnectionTypes = CrossConnectivity.Current.ConnectionTypes.ToList();
                ConnectionHandler.CalculateStrenghts();

            };
            CrossWifiInfo.Current.SignalStrengthChanged += (sender, args) =>
            {
                ConnectionHandler.CalculateStrenghts();

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

        public void GetLocation()
        {

        }





        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
