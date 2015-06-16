using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using CloudStorageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount;

namespace MyWebRole.Common
{
    public class DataQueueStorageHelper
    {

        public static CloudQueue GetQueue()
        {           
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
         
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            queue.CreateIfNotExists();
            return queue;
        }

        public static void InsertQueue(string queue)
        {
            
            CloudQueueMessage message = new CloudQueueMessage(queue);
            GetQueue().AddMessage(message);
        }

        public static List<CloudQueueMessage> GetMessage()
        {
            return  GetQueue().GetMessages(1,TimeSpan.FromSeconds(10)).ToList();
        }

        public static string PeekQueue()
        {
            CloudQueueMessage peekedMessage = GetQueue().PeekMessage();

            return peekedMessage.AsString;
        }

        public static void ChangeMessageContent()
        {
            CloudQueueMessage message = GetQueue().GetMessage();
            message.SetMessageContent("Updated contents.");

            GetQueue().UpdateMessage(message,
                TimeSpan.FromSeconds(0.0), // Make it visible immediately.
                MessageUpdateFields.Content | MessageUpdateFields.Visibility);
        }

        public static void DeQueueMessage()
        {
            // Get the next message
            CloudQueueMessage retrievedMessage = GetQueue().GetMessage();

            //Process the message in less than 30 seconds, and then delete the message
            GetQueue().DeleteMessage(retrievedMessage);
        }

        public static void DeleteQueue()
        {
            GetQueue().Delete();
        }
        

    }
}