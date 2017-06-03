namespace AzureAuthenticationApp.Dependencies
{
    public interface IConnectionInfo
    {
        int GetWifiSignalStrength();
        void CheckWifiConnection();

    }
}