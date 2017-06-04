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
            DownloadQueue = QueueClient.GetQueueReference("responsequeue" + userName.ToLower());


        }



        public async Task UploadWifiDataQueue(string data)
        {
            await UploadQueue.CreateIfNotExistsAsync();
            var message = new CloudQueueMessage(data);
            await UploadQueue.AddMessageAsync(message);
        }

        public async Task<string> PerformRequestQueue(string data)
        {
            await UploadWifiDataQueue(data);
            try
            {
                await DownloadQueue.CreateIfNotExistsAsync();
                CloudQueueMessage message = null;
                for (var i = 0; i < 3; i++)
                {
                    message = await DownloadQueue.GetMessageAsync();
                    if (message != null)
                    {
                        await DownloadQueue.DeleteMessageAsync(message);
                        break;
                    }
                    await Task.Delay(2000);

                }

                return message?.AsString;
            }
            catch (StorageException)
            {
                return null;
            }
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
            catch (StorageException)
            {
                return null;
            }

        }


    }
}
