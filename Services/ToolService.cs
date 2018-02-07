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

        public void SearchForAnagram()
        {

            DictionaryList = _repository.GetData(Name);
            /*Rekursija */
            for (var i = 0; i < Name.Length; i++)
            {
                string NewString = Name;
                Recursion(null, Name[i], NewString, 0, null, 0);
            }
            /*-----*/
        }

        private bool Recursion(string str, char index, string strAllocated, int countingSave, string Zodis, int Pakeisti)
        {
            str += index;
           
           // Console.WriteLine(str);
            string strNew = null;
            foreach (var count in strAllocated)
            {
                if (count == index) { continue; }
                strNew += count;
            }


            /* ---------------------- */
            if (str.Length >= 5)
            {
                if (DictionaryList.Any(stra => stra.Equals(str))) { Console.WriteLine(str); }
                /*  foreach (var word in DictionaryList)
                   {
                  int next = countingSave;
                  if (word == null) { break; }
                  if (word.Length != str.Length) { continue; }
                  //if (CheckingIfAnagram(word, str, strAllocated))
                  //  {

                  if (str.Equals(word))
                  {
                      //Console.WriteLine(str);
                      if(strNew == null)
                      {
                          Console.WriteLine(Zodis + " " + str);
                      }

                      /*Console.WriteLine(str);
                          FindTwoWords(Pakeisti, Zodis, str
                              , countingSave, index, strAllocated, next);
                              */

                //  if (strNew == null) return false;
                //  for (var i = 0; i < strNew.Length; i++)
                // {
                //     Recursion(null, strNew[i], strNew, next, str, 1);
                // }

                //  }
                // }
                // }
                //  }
                ///  if(strNew == null) { return false; }
                //  for (var j = 0; j < strNew.Length; j++)
                // {
                //      Recursion(str, strNew[j], strNew, countingSave, Zodis, 0);  
                // }
            }
            return false;
        }
        private bool FindTwoWords(int Pakeisti, string Zodis,
            string str, int countingSave,  char index, string strAllocated, int next)
        {
            if (Pakeisti == 1 && Zodis != null && ((Zodis.Length + str.Length) == Name.Length))
            {
                //    var sentence = Zodis + " " + str;
                //   Results.Add(sentence);
                Console.WriteLine(Zodis + " " + str);
            }

            if ((str.Length == countingSave) && str.Length != 0)
            {
                return false;
            }

            if (!Recursion(null, index, strAllocated, next, str, 1))
            {
                return false;
            }
            return false;
        }
    }
}