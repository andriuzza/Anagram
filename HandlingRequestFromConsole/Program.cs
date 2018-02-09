using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HandlingRequestFromConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //  DownloadAsyncData();
            ObjectToXml();
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
        public static void ObjectToXml()
        {
            
           
            var path = @"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\produktai.xml";
            RS overview;
            XmlSerializer serializer = new XmlSerializer(typeof(RS));

            using (XmlReader reader = XmlReader.Create(path))
            {
                overview = (RS)serializer.Deserialize(reader);
            }
            Console.WriteLine(overview);

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
