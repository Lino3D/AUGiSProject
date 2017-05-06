using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AzureAuthenticationApp.Dependencies;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureAuthenticationApp.Droid
{



	[Activity (Label = "AzureAuthenticationApp.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
	    private MobileServiceUser user;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init (this, bundle);
		    Xamarin.FormsMaps.Init(this, bundle);
            // Load the main application
            //App.Init(this.Authenticate() as IAuthenticate);
            LoadApplication (new App ());
		}
	    public async Task<bool> Authenticate()
	    {
	        var success = false;
	        var message = string.Empty;
	        try
	        {
	            // Sign in with Facebook login using a server-managed flow.
	            user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
	                MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
	            if (user != null)
	            {
	                message = string.Format("you are now signed-in as {0}.",
	                    user.UserId);
	                success = true;
	            }
	        }
	        catch (Exception ex)
	        {
	            message = ex.Message;
	        }

	        // Display the success or failure message.
	        AlertDialog.Builder builder = new AlertDialog.Builder(this);
	        builder.SetMessage(message);
	        builder.SetTitle("Sign-in result");
	        builder.Create().Show();

	        return success;
	    }
    }
}

