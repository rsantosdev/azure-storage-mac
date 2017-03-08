using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ManageAzureQueue().GetAwaiter().GetResult();
        }

        static async Task ManageAzureQueue()
        {
            var devStorageIp = "192.168.0.25";
            var connectionString = $"DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://{devStorageIp}:10000/devstoreaccount1;TableEndpoint=http://{devStorageIp}:10002/devstoreaccount1;QueueEndpoint=http://{devStorageIp}:10001/devstoreaccount1;";

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var messageQueue = queueClient.GetQueueReference("sample-queue");
            await messageQueue.CreateIfNotExistsAsync();

            var message = new CloudQueueMessage("Hello World!");
            await messageQueue.AddMessageAsync(message);

            var retrievedMessage = await messageQueue.GetMessageAsync();
            var content = retrievedMessage.AsString;
            Console.WriteLine(content);

            await messageQueue.DeleteMessageAsync(retrievedMessage);
        }
    }
}
