using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace MyWebRole.Common
{
    public class DataBlobStorageHelper
    {
        private static string _containerName = RoleEnvironment.GetConfigurationSettingValue("ContainerName");



        private static CloudBlobContainer GetContainer()
        {
            //获取ServiceConfiguration.cscfg配置文件的信息
            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            var client = account.CreateCloudBlobClient();
            //获得BlobContainer对象
            client.GetContainerReference(_containerName)
                    .CreateIfNotExist();
            return client.GetContainerReference(_containerName);
        }
        public static string UploadFile(HttpPostedFileBase imageFile)//string fileName, string contentType, byte[] data)
        {
            //获得BlobContainer对象并把文件上传到这个Container
            var blob = GetContainer().GetBlockBlobReference(imageFile.FileName);
            blob.Properties.ContentType = imageFile.ContentType;

            using (var ms = new MemoryStream(ReadFully(imageFile.InputStream), false))
            {
                blob.UploadFromStream(ms);
            }

            return blob.Uri.AbsoluteUri;
        }
        public static void DeleteFile(string fileUrl)
        {
            CloudBlockBlob blockBlob = GetContainer().GetBlockBlobReference(fileUrl);
            blockBlob.Delete();
        }
        public static CloudBlockBlob FileDetail(string fileUrl)
        {
            CloudBlockBlob blockBlob = GetContainer().GetBlockBlobReference(fileUrl);
            return blockBlob;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static IEnumerable<CloudBlockBlob> GetBlobs()
        {
            var imagesContainer = GetContainer();
            imagesContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });


            List<CloudBlockBlob> cloudBlockBlobs = new List<CloudBlockBlob>();


            foreach (var blobItem in imagesContainer.ListBlobs())
            {

                var blockBlob = imagesContainer.GetBlockBlobReference(blobItem.Uri.AbsoluteUri);
                blockBlob.FetchAttributes();
                cloudBlockBlobs.Add(blockBlob);
            }
            return cloudBlockBlobs;
        }   
    }
}