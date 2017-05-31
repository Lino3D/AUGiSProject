using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using AzureAuthenticationApp.Droid.Models;
using AzureAuthenticationApp.Models.Interfaces;
using AzureAuthenticationApp.Models.UI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
#pragma warning disable 618

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace AzureAuthenticationApp.Droid.Models
{
    public class CustomMapRenderer
        : MapRenderer
    {
        private MapView mapView;

        //private CustomTileProvider tileProvider;
        private CustomMap customMap;

        private bool _isDrawnDone;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;
            mapView = Control;
            customMap = e.NewElement as CustomMap;

            if (customMap != null)
            {
                var tileProvider = new CustomTileProvider(512, 512, customMap.MapTileTemplate);
                var options = new TileOverlayOptions().InvokeTileProvider(tileProvider);

                mapView.Map.AddTileOverlay(options);
            }

            if (mapView?.Map != null)
            {
                mapView.Map.InfoWindowClick += MapOnInfoWindowClick;
            }


            if (customMap != null)
            {
                ((ObservableCollection<Pin>)customMap.Pins).CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePins();
        }

        private void UpdatePins()
        {
            var androidMapView = (MapView)Control;
            var formsMap = (CustomMap)Element;

            androidMapView.Map.Clear();

            androidMapView.Map.MarkerClick += HandleMarkerClick;
            androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

            var items = formsMap.Items;

            foreach (var item in items)
            {
                var markerWithIcon = new MarkerOptions();
                markerWithIcon.SetPosition(new LatLng(item.Location.Latitude, item.Location.Longitude));
                markerWithIcon.SetTitle(string.IsNullOrWhiteSpace(item.Name) ? "-" : item.Name);
                markerWithIcon.SetSnippet(item.Details);

                try
                {
                    markerWithIcon.InvokeIcon(BitmapDescriptorFactory.FromResource(GetPinIcon()));
                }
                catch (Exception)
                {
                    markerWithIcon.InvokeIcon(BitmapDescriptorFactory.DefaultMarker());
                }

                androidMapView.Map.AddMarker(markerWithIcon);
            }
        }

        private int GetPinIcon()
        {
            return Resource.Drawable.student;

        }

        private void HandleMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            var marker = e.Marker;
            marker.ShowInfoWindow();

            var map = this.Element as CustomMap;

            var formsPin = new ExtendedPin(marker.Title, marker.Snippet, marker.Position.Latitude, marker.Position.Longitude);

            map.SelectedPin = formsPin;
        }

        protected override void OnElementPropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;

            if (e.PropertyName == CustomMap.MapTileTemplateProperty.PropertyName)
            {
                UpdateTile();
            }
            if (!e.PropertyName.Equals("VisibleRegion") || _isDrawnDone) return;
            UpdatePins();

            _isDrawnDone = true;
        }
        private void UpdateTile()
        {
            mapView.Map.Clear();
            var tileProvider = new CustomTileProvider(512, 512, customMap.MapTileTemplate);
            var options = new TileOverlayOptions().InvokeTileProvider(tileProvider);
            mapView.Map.AddTileOverlay(options);

        }
        private void MapOnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            Marker clickedMarker = e.Marker;
            // Find the matchin item
            var formsMap = (CustomMap)Element;
            formsMap.ShowDetailCommand.Execute(formsMap.SelectedPin);
        }

        private bool IsItem(IMapModel item, Marker marker)
        {
            return item.Name == marker.Title &&
                   item.Details == marker.Snippet &&
                   item.Location.Latitude == marker.Position.Latitude &&
                   item.Location.Longitude == marker.Position.Longitude;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            //NOTIFY CHANGE

            if (changed)
            {
                _isDrawnDone = false;
            }
        }
    }
}