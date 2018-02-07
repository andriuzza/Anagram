using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ToolService : IServiceTool
    {
        private readonly IRepository<string> _repository;
        private HashSet<string> DictionaryList { get; set; }

        private HashSet<string> Results = new HashSet<string>();
        public string Name { get; private set; }

        public ToolService(IRepository<string> repository, string Name)
        {
            _repository = repository;
            this.Name = Name;
        }


        private bool CheckingIfAnagram(string word, string property, int[] array)
        {
            if (property == null)
            {
                if (word == null) return false;

                if (OrderString(Name).Equals(OrderString(word)))
                {
                    return true;
                }

                return false;
            }

            if (word == null) return false;

            if (OrderString(property).Equals(OrderString(word)))
            {
                return true;
            }

            return false;
        }

        private string OrderString(string name)
        {
            return new string(name.OrderBy(c => c).ToArray());
        }
        public HashSet<string> GetResultsOfAnagram()
        {
            return Results;
        }

        public bool ShowResultsOfAnagram()
        {
            if (!Results.Any())
            {
                return false;
            }
            foreach(var anagram in Results)
            {

                Console.WriteLine(anagram);
            }
            return true;
        }

        public void GetAnagram()
        {
            DictionaryList = _repository.GetData(Name);
            for (var i = 0; i < Name.Length; i++)
            {
                string NewString = Name;
                Recursion(null, Name[i], NewString,  null, 0);
            }

            ShowResultsOfAnagram();
        }

        private bool Recursion(string str, char index, string strAllocated,  string Word, int SecondWord)
        {
            str += index;
           
          
            string strNew = null;
            foreach (var count in strAllocated)
            {
                if (count == index) { continue; }
                strNew += count;
            }

            if (str.Length >= 4)
            {
                foreach (var word in DictionaryList)
                {
                   
                    if (word == null) { break; }
                    if (word.Length != str.Length) { continue; }
                  

                    if (str.Equals(word))
                    {
                        // Console.WriteLine(str);
                        // var sentence = Word + " " + str;
                        if (!str.Equals(Name))
                        {
                            Results.Add(str);
                        }
                           

                        if (strNew == null) return false;
                        for (var i = 0; i < strNew.Length; i++)
                        {
                            Recursion(null, strNew[i], strNew,  str, 1);
                        }
                    }
                }
            }

            if (strNew == null) { return false; }
            for (var j = 0; j < strNew.Length; j++)
            {
                Recursion(str, strNew[j], strNew, Word, 0);
            }

            return false;
        }
        private bool FindTwoWords(int SecondWord, string Word,
            string str,   char index, string strAllocated, int next)
        {
            if (SecondWord == 1 && Word != null && ((Word.Length + str.Length) == Name.Length))
            {
                Console.WriteLine(Word + " " + str);
            }

            if (str.Length != 0)
            {
                return false;
            }

            if (!Recursion(null, index, strAllocated,  str, 1))
            {
                return false;
            }
            return false;
        }
    }
}