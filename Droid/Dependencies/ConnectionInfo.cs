
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Telephony;
using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Droid.Dependencies;
using AzureAuthenticationApp.Droid.Dependencies.Models;
using AzureAuthenticationApp.Models;
using System;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(ConnectionInfo))]
namespace AzureAuthenticationApp.Droid.Dependencies
{
    public class ConnectionInfo : IConnectionInfo
    {
        private GsmSignalStrengthListener signalStrengthListener;
        private TelephonyManager Manager;
        private int GsmStrength;
        private static WifiManager wifi;
        private WifiReceiver wifiReceiver;
        public static List<string> WiFiNetworks;
        private Context context = null;

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

        public IEnumerable<CustomScanData> ReturnScanResults()
        {
            this.context = Application.Context;
            wifi = (WifiManager)context.GetSystemService(Context.WifiService);
            var customResults = new List<CustomScanData>();
            try
            {
                var scanwifinetworks = wifi.ScanResults;
                foreach (var result in scanwifinetworks)
                {
                    customResults.Add(new CustomScanData()
                    {
                        Bssid = result.Bssid,
                        Rssi = result.Level
                    });

                }

                WiFiNetworks = new List<string>();

                // Get a handle to the Wifi


                // Start a scan and register the Broadcast receiver to get the list of Wifi Networks
                wifiReceiver = new WifiReceiver();
                context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
                wifi.StartScan();


                return customResults;







            }
            catch (Exception)
            {

                // ignored
            }
            return null;

        }
        class WifiReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                IList<ScanResult> scanwifinetworks = wifi.ScanResults;
                foreach (ScanResult wifinetwork in scanwifinetworks)
                {
                    WiFiNetworks.Add(wifinetwork.Ssid);
                }
            }
        }
    }
}