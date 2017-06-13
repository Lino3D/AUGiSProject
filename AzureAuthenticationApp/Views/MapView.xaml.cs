using Acr.UserDialogs;
using AzureAuthenticationApp.Helpers;
using AzureAuthenticationApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AzureAuthenticationApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        //TodoItemManager<PositionInfo> manager;
        public ConnectionHandler ConnectionHandler { get; set; }

        public MapViewViewModel ViewModel;
        public MapView()
        {
            InitializeComponent();
            //  manager = TodoItemManager<PositionInfo>.DefaultManager;
            BindingContext = new MapViewViewModel(UserDialogs.Instance);
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(52.22207512938468, 21.006942987442017), Distance.FromMiles(0.1)));
            ViewModel = (MapViewViewModel)BindingContext;

            ViewModel.UserPins.CollectionChanged += UpdatePins;

        }
        private void UpdatePins(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MyMap.UpdatePins(ViewModel.UserPins);
        }
    }
}