using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureAuthenticationApp.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation;

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            MoveToDoListCommand = new Command(MoveToDoList);
            IncreaseCountCommand = new Command(IncreaseCount);
            LoginCommand = new Command(Login);
            OpenMapCommand = new Command(OpenMap);
            CheckWifiCommand = new Command(CheckWifi);
        }

        private int count;

        private string countDisplay = "You clicked 0 times.";

        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        private bool authenticated;

        public ICommand IncreaseCountCommand { get; }

        private void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";

        public ICommand MoveToDoListCommand { get; }

        public async void MoveToDoList()
        {
            await Navigation.PushAsync(new TodoList());
        }

        public bool Authenticated
        {
            get => authenticated;
            set
            {
                authenticated = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public async void Login()
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
        }

        public ICommand OpenMapCommand { get; }

        public async void OpenMap()
        {
            await Navigation.PushAsync(new MapView());
        }

        private string wifiText;

        public ICommand CheckWifiCommand { get; }

        public string WifiText
        {
            get => wifiText;
            set
            {
                wifiText = value;
                OnPropertyChanged();
            }
        }

        public void CheckWifi()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}