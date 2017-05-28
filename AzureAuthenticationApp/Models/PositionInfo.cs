using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AzureAuthenticationApp.Models
{
    public class PositionInfo
    {
        private int id;
        private double lon;
        private double lan;
        private int floornumber;
        private int user_id;

        [JsonProperty(PropertyName = "id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty(PropertyName = "lon")]
        public double Lon { get => lon; set => lon = value; }
        [JsonProperty(PropertyName = "lan")]
        public double Lan { get => lan; set => lan = value; }
        [JsonProperty(PropertyName = "floornumber")]
        public int Floornumber { get => floornumber; set => floornumber = value; }
        [JsonProperty(PropertyName = "userid")]
        public int User_id { get => user_id; set => user_id = value; }

        [Version]
        public string Version { get; set; }
    }
}
