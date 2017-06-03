using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Models;
using Plugin.Connectivity.Abstractions;
using Plugin.WifiInfo;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AzureAuthenticationApp.Helpers
{
    public class ConnectionHandler
    {

        public bool Connected { get; set; }
        public List<ConnectionType> CurrectConnectionTypes { get; set; }
        public IConnectionInfo ConnectionInfo { get; set; }
        private IConnectionInfo _connectionInfo;
        private List<WifiScanData> ScanData { get; }


        public ConnectionHandler()
        {

            Connected = false;
            CurrectConnectionTypes = new List<ConnectionType>();
            _connectionInfo = DependencyService.Get<IConnectionInfo>();
            DependencyService.Get<IConnectionInfo>().CheckWifiConnection();
            ScanData = new List<WifiScanData>();
            CalculateStrenghts();
        }
        public void CalculateStrenghts()
        {

            foreach (var currectConnectionType in CurrectConnectionTypes)
            {
                //dostuff
                if (currectConnectionType == ConnectionType.WiFi)
                {
                    var results = CrossWifiInfo.Current.ScanResults;
                    foreach (var result in results)
                    {
                        ScanData.Add(new WifiScanData()
                        {
                            Bssid = result.Bssid,
                            Rssi = result.Level
                        });
                    }
                }
            }
        }

        public List<WifiScanData> GetScanResults()
        {
            return ScanData.Count > 0 ? ScanData : null;
        }

        public string GetScanResultString()
        {
            StringBuilder results = new StringBuilder();
            foreach (var data in ScanData)
            {
                results.Append(data.ToStringNoColons());
                results.Append("; ");
            }
            return results.ToString();
        }



    }
}
