namespace AzureAuthenticationApp.Dependencies
{
    public interface IConnectionInfo
    {
        bool CheckNetworkConnection();
        int GetGsmSignalStrenght();
        int GetWifiSignalStrength();
        void LaunchListener();
    }
}