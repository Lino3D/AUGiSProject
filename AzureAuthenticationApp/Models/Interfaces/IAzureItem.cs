using Microsoft.WindowsAzure.MobileServices;

namespace AzureAuthenticationApp.Models.Interfaces
{
    public interface IAzureItem
    {
        string Id { get; set; }
        [Version]
        string Version { get; set; }
    }
}