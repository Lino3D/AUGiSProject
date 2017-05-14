using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AzureAuthenticationApp.Models
{
    public class User
    {
        private int id;
        private string firstname;
        private string lastname;
        private string nickname;

        [JsonProperty(PropertyName = "id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty(PropertyName = "firstname")]
        public string Firstname { get => firstname; set => firstname = value; }
        [JsonProperty(PropertyName = "lastname")]
        public string Lastname { get => lastname; set => lastname = value; }
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get => nickname; set => nickname = value; }

        [Version]
        public string Version { get; set; }
      
    }
}
