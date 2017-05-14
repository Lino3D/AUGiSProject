using System;
using AzureAuthenticationApp.Views;

namespace AzureAuthenticationApp.Models
{

    public class MenuDetailViewMenuItem
    {
        public MenuDetailViewMenuItem()
        {
            TargetType = typeof(MenuDetailViewDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}