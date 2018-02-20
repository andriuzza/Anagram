using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository<Word> _repository;
        private HashSet<string> DictionaryHashSet { get; set; }
        private HashSet<string> Results = new HashSet<string>();

        public AnagramSolver(IWordRepository<Word> repository)
        {
            _repository = repository;
        }

        public HashSet<string> GetResultsOfAnagram()
        {
            return Results;
        }

        public HashSet<string> GetAnagram(string Name)
        {
            DictionaryHashSet = _repository.GetData(Name).Select(a=>a.Name) as HashSet<string>;

            for (var i = 0; i < Name.Length; i++)
            {
                Recursion("", Name[i], Name,  Name,  Name, 0);
            }
        
            return Results;
        }

        private bool Recursion(string constructedWord, char letter, string removalWord,
            string Word, string Name, int countWord)
        {
            constructedWord += letter; // appending a new word be a one letter

            string strNew = "";
            bool marked = false;
            foreach (var str in removalWord)
            {
                if (str == letter && marked == false)
                {
                    marked = true;
                    continue;
                }

                strNew += str;
            }        /* constructed word without that letter*/

            var wordOnDictionary = DictionaryHashSet.Contains(constructedWord);
            if (wordOnDictionary && constructedWord.Length > 2)
            {     
                string temporaryWord = "";
                
                if (!constructedWord.Equals(Name)) /*if they are the same not necessary to add */
                {
                    temporaryWord = constructedWord;

                    if (countWord == 0)
                    {
                        countWord++;
                    }
                      
                    if(string.IsNullOrEmpty(strNew) && countWord == 1
                        && (constructedWord.Length) == Name.Length)
                    {
                        Results.Add(constructedWord);
                    }

                    if (string.IsNullOrEmpty(strNew) && countWord == 1 
                        && (Word.Length + constructedWord.Length) == Name.Length)
                    {

                        Results.Add(Word + " " + constructedWord);
                        countWord--;
                    }
                }

                if (string.IsNullOrEmpty(strNew))
                {
                    return true;
                }

                for (var i = 0; i < strNew.Length; i++)
                {
                    Recursion("", strNew[i], strNew, temporaryWord, Name, countWord);
                }
            }

            if (strNew == null) { return false; }

            for (var j = 0; j < strNew.Length; j++)
            {
                Recursion(constructedWord, strNew[j], strNew, Word, Name, countWord);
            }
            return false;
        }

        private bool FindTwoWords(int SecondWord, string Word,
            string str, char letter, string removalWord, string Name)
        {
            if (SecondWord == 1 && Word != null && ((Word.Length + str.Length) == Name.Length))
            {
                Console.WriteLine(Word + " " + str);
            }

            if (str.Length != 0)
            {
                return false;
            }

            if (!Recursion(null, letter, removalWord,  null, Name, 0))
            {
                return false;
            }
            return false;
        }

        private string OrderString(string name)
        {
            return new string(name.OrderBy(c => c).ToArray());
        }
    }
}