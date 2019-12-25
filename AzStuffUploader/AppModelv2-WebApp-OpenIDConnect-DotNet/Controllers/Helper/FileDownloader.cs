using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers.Helper
{
    public static class FileDownloader
    {
        public static async Task<ListEntity> DownloadList()
        {
           //  string connectionString = @"DefaultEndpointsProtocol=https;AccountName=stuffuploader;AccountKey=Xf5tHzrcN959uvni36MSu/cTnAwN1H7Lc2TU/9hJ22H4KkCQrLhffEwrQr+SMvtu2xdGKZK4B1Z9P3u1+V8TKA==;EndpointSuffix=core.windows.net";
           var constr = await SecretManager.OnGetAsync();
           string connectionString =Convert.ToString(constr);
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "testcont";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            var list = cloudBlobContainer.ListBlobs();
            List<string> downloads = list.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();
            ListEntity objlistentity = new ListEntity();
            objlistentity.Files = new List<Entity>();
            
            //cloudBlockBlob.Properties.ContentType = fileMimeType;
           
            // await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
             
            foreach (var data in downloads)
            {
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(data);
                objlistentity.Files.Add(new Entity() { Filenames=data,Url= cloudBlockBlob.Uri.AbsoluteUri});
            }
            return objlistentity;

        }
      
    }
}