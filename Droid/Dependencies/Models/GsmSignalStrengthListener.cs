using Android.Content;
using Android.Telephony;

namespace AzureAuthenticationApp.Droid.Dependencies.Models
{
    [BroadcastReceiver()]
    public class GsmSignalStrengthListener : PhoneStateListener
    {
        public delegate void SignalStrengthChangedDelegate(int strength);

        public event SignalStrengthChangedDelegate SignalStrengthChanged;

        public override void OnSignalStrengthsChanged(SignalStrength newSignalStrength)
        {
            if (newSignalStrength.IsGsm)
            {
                SignalStrengthChanged?.Invoke(newSignalStrength.GsmSignalStrength);
            }
        }
    }
}