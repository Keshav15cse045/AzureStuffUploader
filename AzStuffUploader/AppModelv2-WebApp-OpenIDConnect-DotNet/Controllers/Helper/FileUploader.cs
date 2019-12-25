using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers.Helper
{
    public static class FileUploader
    {
        public static async         Task
UploadFileToBlobAsync(string fileName,string username)
        {
            try
            {
                // string connectionString = @"DefaultEndpointsProtocol=https;AccountName=stuffuploader;AccountKey=Xf5tHzrcN959uvni36MSu/cTnAwN1H7Lc2TU/9hJ22H4KkCQrLhffEwrQr+SMvtu2xdGKZK4B1Z9P3u1+V8TKA==;EndpointSuffix=core.windows.net";
                var constr=await SecretManager.OnGetAsync();
               string connectionString = Convert.ToString(constr);
                //test code
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                string strContainerName = "testcont";
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
                var list = cloudBlobContainer.ListBlobs();
                List<string> blobNames = list.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();
                // string fileName = @"C:\Users\keshav.jha\Desktop\powershell.txt";
                //if (await cloudBlobContainer.CreateIfNotExistsAsync())
                //{
                //    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                //}
                if (fileName != null)
                {
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Path.GetFileName(fileName));
                    //cloudBlockBlob.Properties.ContentType = fileMimeType;
                    cloudBlockBlob.UploadFromFile(fileName);
                    // await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                   // return cloudBlockBlob.Uri.AbsoluteUri;
                }
                

               // return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}