using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Models;
using Plugin.Connectivity.Abstractions;
using Plugin.WifiInfo;
using System.Collections.Generic;
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
                switch (currectConnectionType)
                {
                    case ConnectionType.WiFi:
                        {
                            if (!Connected) return;
                            WifiStrenght = connectionInfo.GetWifiSignalStrength();
                            WifiBssid = CrossWifiInfo.Current.ConnectedWifiInformation.Bssid;
                            break;
                        }
                    case ConnectionType.Cellular:
                        // CellularStrenght = connectionInfo.GetGsmSignalStrenght();
                        break;
                }
            }
        }


    }
}
