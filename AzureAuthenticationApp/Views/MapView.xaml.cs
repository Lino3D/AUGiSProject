using Acr.UserDialogs;
using AzureAuthenticationApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AzureAuthenticationApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();
            BindingContext = new MapViewViewModel(UserDialogs.Instance);

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(52.22207512938468, 21.006942987442017), Distance.FromMiles(0.3)));

               
        }
    }
}