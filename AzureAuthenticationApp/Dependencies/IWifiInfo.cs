namespace AzureAuthenticationApp.Dependencies
{
    public interface IWifiInfoForms
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}