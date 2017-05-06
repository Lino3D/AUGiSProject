using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Droid.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(BaseUrl))]

namespace AzureAuthenticationApp.Droid.Dependencies
{
    public class BaseUrl : IBaseUrl
    {
        public string GetUrl()
        {
            return "file:///android_asset/";
        }
    }
}