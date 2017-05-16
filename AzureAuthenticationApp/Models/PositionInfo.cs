using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureAuthenticationApp.Models.Interfaces;
using Newtonsoft.Json;

namespace AzureAuthenticationApp.Models
{
    public class PositionInfo : IAzureItem
    {
        private string id;
        private double lon;
        private double lat;
        private int floornumber;
        private string user_id;

        public PositionInfo()
        {
        }

        public PositionInfo(double lon, double lat, int floornumber, string user_id)
        {
            this.lon = lon;
            this.lat = lat;
            this.floornumber = floornumber;
            this.user_id = user_id;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get => id; set => id = value; }
        public double Lon { get => lon; set => lon = value; }
        public double Lan { get => lat; set => lat = value; }
        public int Floornumber { get => floornumber; set => floornumber = value; }
        public string User_id { get => user_id; set => user_id = value; }

        [Version]
        public string Version { get; set; }
        public int PropertyName { get; internal set; }
    }
}
