
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
        private TelephonyManager TelManager;
        private int GsmStrength;

        public void CheckWifiConnection()
        {
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi).GetState();
            if (mobileState == NetworkInfo.State.Connected) return;
            var mawifi = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
            mawifi.SetWifiEnabled(true);
        }


        public int GetWifiSignalStrength()
        {
            var manager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
            return manager.ConnectionInfo.Rssi;
        }





        public int GetGsmSignalStrenght()
        {

            TelManager.Listen(signalStrengthListener, PhoneStateListenerFlags.SignalStrength);
            signalStrengthListener.SignalStrengthChanged += HandleSignalStrengthChanged;


            return GsmStrength;
        }


        private void HandleSignalStrengthChanged(int pstrength)
        {
            GsmStrength = pstrength;
        }




        public void LaunchListener()
        {
            TelManager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService);
            signalStrengthListener = new GsmSignalStrengthListener();
            TelManager.Listen(signalStrengthListener, PhoneStateListenerFlags.SignalStrength);
            signalStrengthListener.SignalStrengthChanged += HandleSignalStrengthChanged;
        }



    }
}