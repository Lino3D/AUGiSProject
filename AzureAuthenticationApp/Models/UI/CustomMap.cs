using AzureAuthenticationApp.Extensions;
using AzureAuthenticationApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
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
            get => (string)GetValue(TestProperty);
            set => SetValue(TestProperty, value);
        }

        private static void HandleTestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customMap = (CustomMap)bindable;
            // do something with new value
        }

        public CustomMap(MapSpan region) : base(region)
        {
            LastMoveToRegion = region;
        }

        public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, ExtendedPin>(x => x.SelectedPin, null);

        public ExtendedPin SelectedPin
        {
            get => (ExtendedPin)base.GetValue(SelectedPinProperty);
            set => base.SetValue(SelectedPinProperty, value);
        }

        public ICommand ShowDetailCommand { get; set; }

        private MapSpan _visibleRegion;

        public CustomMap()
        {
            //  throw new NotImplementedException();
        }


        public MapSpan LastMoveToRegion { get; private set; }

        public new MapSpan VisibleRegion
        {
            get => _visibleRegion;
            set
            {
                if (_visibleRegion == value)
                {
                    return;
                }

                OnPropertyChanging("VisibleRegion");
                _visibleRegion = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged("VisibleRegion");
            }
        }

        public ObservableCollection<IMapModel> Items { get; } = new ObservableCollection<IMapModel>();

        public void UpdatePins(IEnumerable<IMapModel> items)
        {
            Pins.Clear();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
                Pins.Add(item.AsPin());
            }
        }
    }
}