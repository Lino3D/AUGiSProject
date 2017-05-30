using AzureAuthenticationApp.Models;
using AzureAuthenticationApp.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;

namespace AzureAuthenticationApp.Extensions
{
    public static class MapExtensions
    {
        public static IList<Pin> ToPins<T>(this IEnumerable<T> items) where T : IMapModel
        {
            return items.Select(i => i.AsPin()).ToList();
        }

        public static Pin AsPin(this IMapModel item)
        {
            var location = item.Location;
            var position = location != null ? new Position(location.Latitude, location.Longitude) : Location.DefaultPosition;
            return new Pin { Label = item.Name, Address = item.Details, Position = position };
        }
    }
}