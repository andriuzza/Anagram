using System;
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

            string path = @"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.Repositories\zodynas.txt";

            AnagramSolver Service = new AnagramSolver(new WordRepository(path));
            Service.GetAnagram(line);
            foreach(var anagram in Service.GetResultsOfAnagram())
            {
                Console.WriteLine(anagram);
            }
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