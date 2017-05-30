using AzureAuthenticationApp.Constants;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureAuthenticationApp.Helpers
{
    public class BlobContainerManager
    {
        public CloudStorageAccount StorageAccount { get; }
        public CloudBlobClient BlobClient;
        private CloudBlobContainer _uploadContainer;
        private CloudBlobContainer _downloadContainer;
        private string userName;

        public BlobContainerManager(string userName)
        {
            StorageAccount = CloudStorageAccount.Parse(AppConstants.StorageConnection);
            BlobClient = StorageAccount.CreateCloudBlobClient();

            this.userName = userName;
            _uploadContainer = BlobClient.GetContainerReference(AppConstants.Gismaincontainer);
            _downloadContainer = BlobClient.GetContainerReference(AppConstants.Gismycontainer);
        }

        public async Task UploadWifiData(int strength, string bssid, string seconds)
        {

            await _uploadContainer.CreateIfNotExistsAsync();

            var blockBlob = _uploadContainer.GetBlockBlobReference("posdatarequest" + userName + seconds);

            await blockBlob.UploadTextAsync(strength + ";" + bssid);

        }

        public async Task<string> DownloadPositionData(string seconds)
        {
            var blockBlob = _downloadContainer.GetBlockBlobReference("response-" + userName + seconds);
            var options = new BlobRequestOptions()
            {
                RetryPolicy = new LinearRetry(TimeSpan.FromMilliseconds(500), 5),
                MaximumExecutionTime = TimeSpan.FromSeconds(2)
            };
            var text = await blockBlob.DownloadTextAsync(Encoding.UTF8, AccessCondition.GenerateEmptyCondition(), options, null);
            return text;
        }
        public async Task<bool> DeleteFileAsync(string name)
        {
            var blob = _uploadContainer.GetBlobReference(name);
            return await blob.DeleteIfExistsAsync();
        }


        public async Task<string> PerformPositionRequest(int strength, string bssid)
        {
            var seconds = DateTime.Now.Second.ToString();
            await UploadWifiData(strength, bssid, seconds);
            try
            {
                return await DownloadPositionData(seconds);
            }
            catch (StorageException e)
            {
                return null;
            }

        }


    }
}
