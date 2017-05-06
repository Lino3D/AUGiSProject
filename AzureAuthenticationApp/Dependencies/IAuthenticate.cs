using System.Threading.Tasks;

namespace AzureAuthenticationApp.Dependencies
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}