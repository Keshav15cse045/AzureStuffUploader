using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers.Helper
{
    public static class SecretManager
    {
        public static async Task<string> OnGetAsync()
        {
            string Message = "Your application description page.";
            try
            {
                string keyVaultUrl= System.Configuration.ConfigurationManager.AppSettings["keyvaultUrl"];
                Console.WriteLine($"retrieving details..........");
                /* The next four lines of code show you how to use AppAuthentication library to fetch secrets from your key vault */
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(keyVaultUrl)
                       .ConfigureAwait(false);
                //var secret = await keyVaultClient.GetSecretAsync("url")
                //        .ConfigureAwait(false);
                Message = secret.Value;
                Console.WriteLine(Message);
            }
            /* If you have throttling errors see this tutorial https://docs.microsoft.com/azure/key-vault/tutorial-net-create-vault-azure-web-app */
            /// <exception cref="KeyVaultErrorException">
            /// Thrown when the operation returned an invalid status code
            /// </exception>
            catch (KeyVaultErrorException keyVaultException)
            {
                Message = keyVaultException.Message;
            }
            return Message;
        }
    }
}