using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anagrams.Repositories
{
    public class FileReadingRepository : IRepository<string>
    {
        private System.IO.StreamReader file = null;
        public HashSet<string> ListHash { get; private set; }
        
        public HashSet<string> GetData(string Name)
        {
            if (ListHash == null)
            {
                ListHash = new HashSet<string>();

                string line;
                try
                {
                    file = new System.IO
                       .StreamReader(@"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams\zodynas.txt", System.Text.Encoding.UTF8, true);
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong directory or other problems with reading");
                }
                while ((line = file.ReadLine()) != null)
                {
                    if (line[0] >= '0' && line[0] <= '9')
                    {
                        continue;
                    }

                    var wordsOfLine = Parsing(line);
                    int yra = 0;
                    int yra2 = 0;
                    foreach (var a in wordsOfLine.Item1.ToLower())
                    {
                        if (!Name.Contains(a)) { yra = 1; break; }

                    }

                    if (wordsOfLine.Item2 != null)
                    {
                        foreach (var a in wordsOfLine.Item2.ToLower())
                        {
                            if (!Name.Contains(a)) { yra2 = 1; break; }
                        }
                    }
                    if (yra == 0)
                    {
                        ListHash.Add(wordsOfLine.Item1);
                    }

                    if (yra2 == 0 && wordsOfLine.Item2 != null)
                    {
                        ListHash.Add(wordsOfLine.Item2);
                    }
                }
                return ListHash;
            }
            

            return ListHash;
        }

        private Tuple<string, string> Parsing(string line)
        {
            string nameFirst = null;
            string nameSecond = null;

            for (var i = 0; i < line.Length; i++)
            {
                char letter = line[i];
                if (letter != '	')
                {
                    nameFirst += line[i];
                    continue;
                }
                break;
            }

            for (var i = line.Length - 3; i >= 0; i--)
            {
                if (line[i] != '	')
                {
                    nameSecond += line[i];
                    continue;
                }
                break;
            }
            return new Tuple<string, string>
                (nameFirst, ReverseString(nameSecond));
        }

        private string ReverseString(string name)
        {
            if (name == null) { return null; }
            string newString = "";

            for (var i = name.Length - 1; i >= 0; i--)
            {
                newString += name[i];
            }
            return newString;
        }
    }
}
