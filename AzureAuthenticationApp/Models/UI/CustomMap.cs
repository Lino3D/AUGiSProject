using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AzureAuthenticationApp.Models.UI
{
    public class CustomMap : Map
    {
        public static BindableProperty MapTileTemplateProperty = BindableProperty.Create("MapTile", typeof(string), typeof(InputView),
            "http://samorzad.mini.pw.edu.pl/plan2/images/Pietro0/{z}/{x}/{y}.png");

        public string MapTileTemplate
        {
            get => (string)GetValue(MapTileTemplateProperty);
            set => SetValue(MapTileTemplateProperty, value);
        }
        public static BindableProperty TestProperty = BindableProperty.Create(
            "Test",
            typeof(string),
            typeof(CustomMap),
            "test",
            BindingMode.OneWay,
            propertyChanged: HandleTestChanged);

        public string Test
        {
            get { return (string)GetValue(TestProperty); }
            set { SetValue(TestProperty, value); }
        }

        private static void HandleTestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customMap = (CustomMap)bindable;
            // do something with new value
        }
    }
}