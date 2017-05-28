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
        private double lan;
        private int floornumber;
        private int user_id;

        [JsonProperty(PropertyName = "id")]
        public string Id { get => id; set => id = value; }
        public double Lon { get => lon; set => lon = value; }
        public double Lan { get => lan; set => lan = value; }
        public int Floornumber { get => floornumber; set => floornumber = value; }
        [JsonProperty(PropertyName = "userid")]
        public int User_id { get => user_id; set => user_id = value; }

        [Version]
        public string Version { get; set; }
    }
}
