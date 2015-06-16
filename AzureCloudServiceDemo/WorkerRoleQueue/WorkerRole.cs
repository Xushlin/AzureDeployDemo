using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WorkerRoleQueue
{
    public class WorkerRole : RoleEntryPoint
    {
        private CloudQueue queue;
    
        public override void Run()
        {         
            CloudQueueMessage msg = null;

            while (true)
            {
                try
                {
                    msg = queue.GetMessage();
                    if (msg != null)
                    {
                        ProcessQueueMessage(msg);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (StorageException e)
                {
                    if (msg != null && msg.DequeueCount > 5)
                    {
                        queue.DeleteMessage(msg);
                    }
                    Thread.Sleep(5000);
                }
            }
        }

        public override bool OnStart()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            queue = queueClient.GetQueueReference("myqueue");

            queue.CreateIfNotExists();
            return true;
        }

        public override void OnStop()
        {
            
        }

        private void ProcessQueueMessage(CloudQueueMessage msg)
        {
            Trace.TraceInformation(msg.AsString);
            queue.DeleteMessage(msg);
        }
    }
}
