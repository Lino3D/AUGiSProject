using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using AzureAuthenticationApp.Droid.Models;
using AzureAuthenticationApp.Models;
using AzureAuthenticationApp.Models.UI;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace AzureAuthenticationApp.Droid.Models
{
    public class CustomMapRenderer
        : MapRenderer
    {
        private MapView map;

        //private CustomTileProvider tileProvider;
        private CustomMap customMap;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                map = Control as MapView;
                customMap = e.NewElement as CustomMap;

                var tileProvider = new CustomTileProvider(512, 512, customMap.MapTileTemplate);
                var options = new TileOverlayOptions().InvokeTileProvider(tileProvider);

                map.Map.AddTileOverlay(options);
            }
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

        }
        private void UpdateTile()
        {

            var tileProvider = new CustomTileProvider(512, 512, customMap.MapTileTemplate);
            var options = new TileOverlayOptions().InvokeTileProvider(tileProvider);

            map.Map.AddTileOverlay(options);
        }
    }
}