using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Anagrams.Repositories
{
    public class WordRepository : IWordRepository<string>
    {
        private StreamReader File = null;
        public HashSet<string> ListHash { get; private set; }
        public string filePath { get; private set; }

        public WordRepository(string path)
        {
            filePath = path;
        }

        public HashSet<string> GetData(string Name = null)
        {
           ListHash = new HashSet<string>();
           string line;

            try
            {
                File = NewFileHandling(filePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong directory or other problems with reading");
            }
            while ((line = File.ReadLine()) != null)
            {
                if (line[0] >= '0' && line[0] <= '9')
                {
                    continue;
                }

                var wordsOfLine = Parsing(line);

                if (Name == null) /*If parameter is null, get all words from dictionary */
                {
                    ListHash.Add(wordsOfLine.Item1);
                    ListHash.Add(wordsOfLine.Item2);
                    continue;
                }

                bool ifContains = false;
                bool ifContains2 = false;
                foreach (var a in wordsOfLine.Item1.ToLower())
                {
                    if (!Name.Contains(a)) { ifContains = true; break; }

                }

                if (wordsOfLine.Item2 != null)
                {
                    foreach (var a in wordsOfLine.Item2.ToLower())
                    {
                        if (!Name.Contains(a)) { ifContains2 = true; break; }
                    }
                }

                if (!ifContains)
                {
                    ListHash.Add(wordsOfLine.Item1);
                }

                if (ifContains2 == false && wordsOfLine.Item2 != null)
                {
                    ListHash.Add(wordsOfLine.Item2);
                }
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

        public bool InsertNewWord(string Name)
        {
           /* using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("eina sau");
            }*/
            return false;
        }

        private StreamReader NewFileHandling(string filePath)
        {
            return new System.IO
                    .StreamReader(filePath, System.Text.Encoding.UTF8, true);
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
