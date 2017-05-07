using System;
using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Views;
using Xamarin.Forms;
namespace AzureAuthenticationApp
{
    public partial class App : Application
	{
		public App ()
		{
			// The root page of your application
		   MainPage = new NavigationPage( new AzureAuthenticationApp.Views.MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	    public static IAuthenticate Authenticator { get; private set; }

	    public static void Init(IAuthenticate authenticator)
	    {
	        Authenticator = authenticator;
	    }
    }
}

