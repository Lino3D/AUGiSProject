using System;
using System.Linq;
using Acr.UserDialogs;
using AzureAuthenticationApp.Helpers;
using AzureAuthenticationApp.Models;
using AzureAuthenticationApp.ViewModels;
using Plugin.Connectivity;
using Plugin.WifiInfo;
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

        public MapView()
        {
            InitializeComponent();
          //  manager = TodoItemManager<PositionInfo>.DefaultManager;
            BindingContext = new MapViewViewModel(UserDialogs.Instance);
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(52.22207512938468, 21.006942987442017), Distance.FromMiles(0.1)));

        }
        private async void LocationButton_OnClicked(object sender, EventArgs e)
        {
      //     // var locator = CrossGeolocator.Current;
      //      var position = await locator.GetPositionAsync(1000);
      //      if (position == null) return;
      //      var lat = position.Latitude;
      //      var lon = position.Longitude;
      //      var message = string.Format("Lat: {0}, Lon {1}", lat, lon);
      //      DisplayAlert("Your Position, adding to azure" , message, "Cancel");

      //     // var posInfo = new PositionInfo(lon, lat, 0, "0");
      //      var pos = new Position(lat, lon);
      //      var pin = new Pin
      //      {
      //          Type = PinType.Place,
      //          Position = pos,
      //          Label = "custom pin",
      //          Address = "custom detail info"
      //      };
      //      MyMap.Pins.Add(pin);
      //  //    MyMap.MoveToRegion(
      ////          MapSpan.FromCenterAndRadius( pos, Distance.FromMiles(0.3)));
      // //     await manager.SaveAsync(posInfo);
        }
    }
}