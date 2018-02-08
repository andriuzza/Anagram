using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandlingRequestFromConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadAsyncData();
        }
        static async void DownloadAsyncData()
        {
            /* Part generate request from commande app to asp.net mvc WEB API (to IHttpActionResult)
             * -------WEB REQUESTS--------*/

            HttpClient _client = new HttpClient();

            var result = await GetAnagram
                ("http://localhost:54566/api/word/?name=labadiena", _client);

            Console.WriteLine(result);

        }
        public static async Task<string> GetAnagram(string path, HttpClient _client)
        {
            string responseBody = null;

            var response = Task.Run(async () => { return await _client.GetAsync(path); }).Result; 
            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
        }

    }
}
