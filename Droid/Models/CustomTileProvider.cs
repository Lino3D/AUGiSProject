﻿using Android.Gms.Maps.Model;
using System;

namespace AzureAuthenticationApp.Droid.Models
{
    public class CustomTileProvider : UrlTileProvider
    {
        private string urlTemplate;

        public CustomTileProvider(int x, int y, string urlTemplate)
            : base(x, y)
        {
            this.urlTemplate = urlTemplate;
        }

        public override Java.Net.URL GetTileUrl(int x, int y, int z)
        {
            var url = urlTemplate.Replace("{z}", z.ToString()).Replace("{x}", x.ToString()).Replace("{y}", y.ToString());
            Console.WriteLine(url);
            return new Java.Net.URL(url);
        }
    }
}