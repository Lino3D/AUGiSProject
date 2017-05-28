
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Telephony;
using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Droid.Dependencies;
using AzureAuthenticationApp.Droid.Dependencies.Models;

[assembly: Xamarin.Forms.Dependency(typeof(ConnectionInfo))]
namespace AzureAuthenticationApp.Droid.Dependencies
{
    public class ConnectionInfo : IConnectionInfo
    {
        private GsmSignalStrengthListener signalStrengthListener;
        private TelephonyManager Manager;
        private int GsmStrength;

        public bool CheckNetworkConnection()
        {
            var connectivityManager =
                (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            return activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting;
        }

        public int GetGsmSignalStrenght()
        {

            Manager.Listen(signalStrengthListener, PhoneStateListenerFlags.SignalStrength);
            signalStrengthListener.SignalStrengthChanged += HandleSignalStrengthChanged;


            return GsmStrength;
        }

        private void HandleSignalStrengthChanged(int pstrength)
        {
            GsmStrength = pstrength;
        }


        public int GetWifiSignalStrength()
        {
            var manager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
            return manager.ConnectionInfo.Rssi;
        }

        public void LaunchListener()
        {
            Manager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService);
            signalStrengthListener = new GsmSignalStrengthListener();
            Manager.Listen(signalStrengthListener, PhoneStateListenerFlags.SignalStrength);
            signalStrengthListener.SignalStrengthChanged += HandleSignalStrengthChanged;
        }
    }
}