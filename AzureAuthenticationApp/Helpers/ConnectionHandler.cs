using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Models;
using Plugin.Connectivity.Abstractions;
using System.Collections.Generic;
using Plugin.WifiInfo;
using Xamarin.Forms;

namespace AzureAuthenticationApp.Helpers
{
    public class ConnectionHandler
    {

        public bool Connected { get; set; }
        public List<ConnectionType> CurrectConnectionTypes { get; set; }
        public RoomLocator Locator { get; set; }
        public IConnectionInfo ConnectionInfo { get; set; }

        public string WifiBssid { get; set; }
        public int WifiStrenght { get; set; }
        public int CellularStrenght { get; set; }
        private IConnectionInfo connectionInfo;



        public ConnectionHandler()
        {
            Connected = false;
            CurrectConnectionTypes = new List<ConnectionType>();
            Locator = new RoomLocator();
            connectionInfo = DependencyService.Get<IConnectionInfo>();
            connectionInfo.LaunchListener();
        }
        public void CalculateStrenghts()
        {

            foreach (var currectConnectionType in CurrectConnectionTypes)
            {
                //dostuff
                if (currectConnectionType == ConnectionType.WiFi)
                {
                    if (!Connected) return;
                    WifiStrenght = connectionInfo.GetWifiSignalStrength();
                    WifiBssid = CrossWifiInfo.Current.ConnectedWifiInformation.Bssid;
                }
                else if (currectConnectionType == ConnectionType.Cellular)
                {
                    // CellularStrenght = connectionInfo.GetGsmSignalStrenght();
                }
            }
        }


    }
}
