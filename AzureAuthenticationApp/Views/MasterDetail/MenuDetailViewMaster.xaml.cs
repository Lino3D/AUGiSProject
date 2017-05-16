using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AzureAuthenticationApp.Models;
using AzureAuthenticationApp.Models.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzureAuthenticationApp.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetailViewMaster : ContentPage
    {
        public ListView ListView => ListViewMenuItems;

        public MenuDetailViewMaster()
        {
            InitializeComponent();
            BindingContext = new MenuDetailViewMasterViewModel();
        }

        class MenuDetailViewMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuDetailViewMenuItem> MenuItems { get; }
            public MenuDetailViewMasterViewModel()
            {
                MenuItems = new ObservableCollection<MenuDetailViewMenuItem>(new[]
                {
                    new MenuDetailViewMenuItem { Id = 0, Title = "Mapa", TargetType = typeof(MapView) },
                    new MenuDetailViewMenuItem { Id = 1, Title = "Menu", TargetType = typeof(MainPage)},
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            #endregion
        }
    }
}