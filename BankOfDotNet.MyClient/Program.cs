using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankOfDotNet.MyClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var discoRo = await HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(new HttpClient(), "http://localhost:5000");
            if (discoRo.IsError)
            {
                Console.WriteLine(discoRo.Error);
                return;
            }

            // get token
            var tokenClientRo = new TokenClient(discoRo.TokenEndpoint, "ro.client", "secret");
            var tokenRespRo = await tokenClientRo.RequestResourceOwnerPasswordAsync("Dhanraj", "gehlot", "bankOfDotNetApi");
            if (tokenRespRo.IsError)
            {
                Console.WriteLine(tokenRespRo.Error);
                return;
            }

            Console.WriteLine(tokenRespRo.Json);
            Console.WriteLine("\n\n\n");




            var disco = await HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(new HttpClient(), "http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // get token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "Client", "secret");
            var tokenResp = await tokenClient.RequestClientCredentialsAsync("bankOfDotNetApi");
            if (tokenResp.IsError)
            {
                Console.WriteLine(tokenResp.Error);
                return;
            }

            Console.WriteLine(tokenResp.Json);
            Console.WriteLine("\n\n\n");

            // Call API
            var client = new HttpClient();
            client.SetBearerToken(tokenResp.AccessToken);

            var custInfo = new StringContent(
                JsonConvert.SerializeObject(
                    new
                    {
                        Id = 10,
                        FirstName = "Dhanraj",
                        LastName = "Gehlot"
                    }), Encoding.UTF8, "application/json");

            var responce = await client.PostAsync("http://localhost:52117/api/customers", custInfo);

            if (!responce.IsSuccessStatusCode)
            {
                Console.WriteLine(responce.StatusCode);
            }

            var getResponce = await client.GetAsync("http://localhost:52117/api/customers");
            if (!getResponce.IsSuccessStatusCode)
            {
                Console.WriteLine(responce.StatusCode);
            }
            else
            {
                var cust = await getResponce.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(cust));
            }
        }
    }
}
