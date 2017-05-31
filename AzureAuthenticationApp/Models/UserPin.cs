using AzureAuthenticationApp.Models.Interfaces;

namespace AzureAuthenticationApp.Models
{
    public class UserPin : IMapModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public ILocation Location { get; set; }
        public string ImageUrl { get; set; }
    }
}

