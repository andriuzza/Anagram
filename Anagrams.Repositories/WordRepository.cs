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
        private HashSet<string> HashSetHash = new HashSet<string>();
        public string filePath { get; private set; }

        public WordRepository(string path)
        {
            filePath = path;
            GetDataInitialize();
        }

        public string ReturnFilePath()
        { 
            return filePath;
        }

        public HashSet<string> GetData(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                HashSet<string> newDictionary = new HashSet<string>();
                foreach (var word in HashSetHash)
                {
                    if (CheckIfWordHasSameLetters(word, name))
                    {
                        newDictionary.Add(word);
                    }
                }
                return newDictionary;
            }
            return HashSetHash;
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

        private void GetDataInitialize()
        {
            string line;

            File = NewFileHandling(filePath);

            while ((line = File.ReadLine()) != null)
            {
                if (line[0] >= '0' && line[0] <= '9')
                {
                    continue;
                }
                var wordsOfLine = Parsing(line);

                HashSetHash.Add(wordsOfLine.Item1);
                HashSetHash.Add(wordsOfLine.Item2);
            }
        }
        private bool CheckIfWordHasSameLetters(string wordFromDictionary, string name)
        {
            if(wordFromDictionary == null)
            {
                return false;
            }

            if (name == null) /*If parameter is null, get all words from dictionary */
            {
                return false;
            }

            if(name.Length < wordFromDictionary.Length)
            {
                return false;
            }

            if(wordFromDictionary.Length > 12)
            {
                return false;
            }

            bool ifContains = false;
            foreach (var a in wordFromDictionary.ToLower())
            {
                if (!name.Contains(a))
                {
                    ifContains = true;
                    break;
                }
            }

            if (!ifContains)
            {
                return true;
            }

            return false;
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

        private StreamReader NewFileHandling(string filePath)
        {
            try
            {
                return new System.IO
                        .StreamReader(filePath, System.Text.Encoding.UTF8, true);
            }
            catch (Exception)
            {
                throw new NullReferenceException("Path is wrong or file type " +
                    "is not correct, must be with .txt extension");   
            }
        }

        private string ReverseString(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            string newString = "";

            for (var i = name.Length - 1; i >= 0; i--)
            {
                newString += name[i];
            }
            return newString;
        }
        public HashSet<string> Contains(string Name)
        {
            return null;
        }
    }
}
/*
 /* if (line[0] >= '0' && line[0] <= '9')
                {
                    continue;
                }

                var wordsOfLine = Parsing(line);

                if (Name == null) If parameter is null, get all words from dictionary 
                {
                    HashSetHash.Add(wordsOfLine.Item1);
                    HashSetHash.Add(wordsOfLine.Item2);
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
                    HashSetHash.Add(wordsOfLine.Item1);
                }

                if (ifContains2 == false && wordsOfLine.Item2 != null)
                {
                    HashSetHash.Add(wordsOfLine.Item2);
                }
    */ 