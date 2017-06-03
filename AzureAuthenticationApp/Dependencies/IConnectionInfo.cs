using AzureAuthenticationApp.Models;
using System.Collections.Generic;

namespace AzureAuthenticationApp.Dependencies
{
    public interface IConnectionInfo
    {
        bool CheckNetworkConnection();
        int GetGsmSignalStrenght();
        int GetWifiSignalStrength();
        void LaunchListener();
        IEnumerable<CustomScanData> ReturnScanResults();
    }
}