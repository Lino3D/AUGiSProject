namespace AzureAuthenticationApp.Models
{
    public class WifiScanData
    {
        public WifiScanData()
        {
        }

        public WifiScanData(string bssid, int rssi)
        {
            Bssid = bssid;
            Rssi = rssi;
        }

        public string Bssid { get; set; }
        public int Rssi { get; set; }

        public string ToStringNoColons()
        {
            return Bssid.Replace(":", "") + ", " + Rssi;
        }
    }
}
