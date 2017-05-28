using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAuthenticationApp.Models
{
    public class AccessPoint
    {
        public string Bssid { get; set; } = "";

        public string KeyName { get; set; } = "";

        public string RoomName { get; set; } = "";
    }
}
