using AzureAuthenticationApp.Constants;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureAuthenticationApp.Helpers
{
    public class AzureContainerManager
    {
        public CloudStorageAccount StorageAccount { get; }
        public CloudBlobClient BlobClient;
        public CloudQueueClient QueueClient;
        private CloudBlobContainer _uploadContainer;
        private CloudBlobContainer _downloadContainer;
        private CloudQueue UploadQueue;
        private CloudQueue DownloadQueue;
        private string userName;

        public AzureContainerManager(string userName)
        {
            StorageAccount = CloudStorageAccount.Parse(AppConstants.StorageConnection);
            BlobClient = StorageAccount.CreateCloudBlobClient();
            QueueClient = StorageAccount.CreateCloudQueueClient();
            this.userName = userName;
            _uploadContainer = BlobClient.GetContainerReference(AppConstants.Gismaincontainer);
            _downloadContainer = BlobClient.GetContainerReference(AppConstants.Gismycontainer);
            UploadQueue = QueueClient.GetQueueReference("messagequeue" + userName.ToLower());


        }

        public async Task UploadWifiData(int strength, string bssid, string seconds)
        {

            await _uploadContainer.CreateIfNotExistsAsync();

            var blockBlob = _uploadContainer.GetBlockBlobReference("posdatarequest" + userName + seconds);

            await blockBlob.UploadTextAsync(strength + ";" + bssid);

        }

        public async Task UploadWifiDataQueue(int strength, string bssid)
        {
            await UploadQueue.CreateIfNotExistsAsync();
            var message = new CloudQueueMessage(bssid);
            await UploadQueue.AddMessageAsync(message);
        }

        public async Task<string> PerformRequestQueue(int strength, string bssid)
        {
            await UploadWifiDataQueue(strength, bssid);
            var options = new QueueRequestOptions()
            {
                RetryPolicy = new LinearRetry(TimeSpan.FromMilliseconds(1000), 3),
                MaximumExecutionTime = TimeSpan.FromSeconds(2)
            };
            try
            {
                await Task.Delay(1000);
                var peekedMessage = await UploadQueue.PeekMessageAsync(options, null);
                return peekedMessage?.AsString;
            }
            catch (StorageException)
            {
                return null;
            }
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
            catch (StorageException)
            {
                return null;
            }

        }


    }
}
