using AzureAuthenticationApp.Constants;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace AzureAuthenticationApp.Helpers
{
    public class BlobContainerManager
    {
        public CloudStorageAccount StorageAccount { get; }
        public CloudBlobClient BlobClient;
        private CloudBlobContainer container;
        private string userName;

        public BlobContainerManager(string userName)
        {
            StorageAccount = CloudStorageAccount.Parse(AppConstants.StorageConnection);
            BlobClient = StorageAccount.CreateCloudBlobClient();
            this.userName = userName;
        }

        public async void UploadWifiData(int strength, string bssid)
        {
            container = BlobClient.GetContainerReference(AppConstants.Gismaincontainer);
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("posdatarequest" + userName + DateTime.Now.Millisecond);

            await blockBlob.UploadTextAsync(strength + ";" + bssid);

        }

        public async void ObtainPositionData()
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("posdatarequest" + userName + DateTime.Now.Millisecond + "response");
            await blockBlob.DownloadTextAsync();
        }


    }
}
