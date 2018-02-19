using System;
using Services;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Anagrams_Repositories;

namespace Anagrams
{
    class Program
    {
        static void Main(string[] args)
        {
            TransferToDataBase(); 
        }
        public static void TransferToDataBase()
        {
            DatabaseInit db = new DatabaseInit(new WordRepository());
            db.TransferToDataBase();
        }
        public static void Show()
        {
            Console.WriteLine("Insert words");
            string line = GetWithoutWhiteSpace(Console.ReadLine());
            string path = @"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\zodynas.txt";

            var sw = Stopwatch.StartNew();

            AnagramSolver Service = new AnagramSolver(new WordRepository());
            Service.GetAnagram(line);
            foreach (var anagram in Service.GetResultsOfAnagram())
            {
                Console.WriteLine(anagram);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
        static async void MainAsync()
        {
            /* 1 Part, generate anagrams from console
             *
             * Console.WriteLine("Insert words");
             string line = GetWithoutWhiteSpace(Console.ReadLine());

             string path = @"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\zodynas.txt";

             AnagramSolver Service = new AnagramSolver(new WordRepository(path));
             Service.GetAnagram(line);
             foreach(var anagram in Service.GetResultsOfAnagram())
             {
                 Console.WriteLine(anagram);
             }*/
            /* 3 Part generate request from commande app to asp.net mvc(to actoin links)
             * -------WEB REQUESTS--------*/

            HttpClient _client = new HttpClient();
            var result = await GetAnagram("http://localhost:54566/api/word/?name=labadiena", _client);
            
            Console.WriteLine(result);

        }
        public static async Task<string> GetAnagram(string path, HttpClient _client)
        {
            string responseBody = null;

            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }


        public static string GetWithoutWhiteSpace(string line)
        {
            string newLine = null;
            for (int str = 0; str < line.Length; str++)
            {
                if (line[str] == ' ')
                {
                    continue;
                }
                newLine += line[str];
            }
            return newLine;
        }
    }
}