using AzureAuthenticationApp.Constants;
using AzureAuthenticationApp.Dependencies;
using AzureAuthenticationApp.Views.MasterDetail;
using Microsoft.WindowsAzure.Storage;
using Xamarin.Forms;
namespace AzureAuthenticationApp
{
    public partial class App : Application
    {
        public App()
        {
            // The root page of your application
            InitStorage();

            MainPage = new MenuDetailView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }
        public static CloudStorageAccount StorageAccount { get; private set; }

        public static async void InitStorage()
        {
            // Retrieve storage account from connection string.
            StorageAccount = CloudStorageAccount.Parse(AppConstants.StorageConnection);

            //// Create the blob client.
            //CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            //// Retrieve reference to a previously created container.
            //CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            //// Create the container if it doesn't already exist.
            //await container.CreateIfNotExistsAsync();

            //// Retrieve reference to a blob named "myblob".
            //CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            //// Create the "myblob" blob with the text "Hello, world!"
            //await blockBlob.UploadTextAsync("Hello, world!");
        }

    }
}

