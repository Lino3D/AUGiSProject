using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.WifiInfo.Abstractions;

namespace AzureAuthenticationApp.Models
{
    public class RoomLocator
    {
        private static Dictionary<String, AccessPoint> accessPointsRoomList = new Dictionary<String, AccessPoint>();

        public RoomLocator()
        {
         //   accessPointsRoomList.Add("90:8d:78:75:e7:3c", new AccessPoint() { Mac = "90:8d:78:75:e7:3c", RoomName = "Room 1", KeyName = "" });

          //  accessPointsRoomList.Add("01:80:c2:00:00:03", new AccessPoint() { Mac = "01:80:c2:00:00:03", RoomName = "Room 2", KeyName = "" });

        }
        public AccessPoint GetMyAccessPointDetails(WifiAccessPoinntScanResult availableAccesspoints)
        {

            AccessPoint nearestAccessPoint = null;

            if (accessPointsRoomList.ContainsKey(availableAccesspoints.Bssid.ToLower()))
            {

                accessPointsRoomList.TryGetValue(availableAccesspoints.Bssid.ToLower(), out nearestAccessPoint);

            }
            return nearestAccessPoint;

        }

    }

}
