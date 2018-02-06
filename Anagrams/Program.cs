using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Services;
using Anagrams.Repositories;

namespace Anagrams
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert words");
            string line = GetWithoutWhiteSpace(Console.ReadLine());

            ToolService Service = new ToolService(new FileReadingRepository(), line);
            Service.SearchForAnagram();
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