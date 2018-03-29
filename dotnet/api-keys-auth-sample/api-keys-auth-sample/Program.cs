using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace api_keys_auth_sample
{
    public class Program
    {
        private static readonly string ApiKey = "[YOUR_API_KEY]";
        private static readonly string APIUrl = "https://livestreamapis.com/v1/accounts";

        private static void Main(string[] args)
        {
            var result = GetAccounts();

            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static async Task<string> GetAccountsAsync()
        {
            using (var client = new HttpClient())
            {
                var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApiKey}:"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                var response = await client.GetAsync(APIUrl);
                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {
                    //var resultContent = response.Content.ReadAsStringAsync().Result;
                    //return resultContent;
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
            }
        }

        private static string GetAccounts()
        {
            var request = WebRequest.Create(APIUrl);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiKey + ":"));
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
