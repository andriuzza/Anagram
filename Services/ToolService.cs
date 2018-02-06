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

        public bool CheckingIfAnagram(string word, string property, int[] array)
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

        public string OrderString(string name)
        {
            return new string(name.OrderBy(c => c).ToArray());
        }

        public void SearchForAnagram()
        {
          DictionaryList =  _repository.GetData(Name);
            /*Rekursija */
            for (var i = 0; i < Name.Length; i++)
            {
                int[] MyArray = new int[Name.Length];
                Recursion(null, i, MyArray, 0, null, 0);
            }
            /*-----*/

         /*   foreach (var word in DictionaryList.ToArray())
            {
                CheckingIfAnagram(word, null, null);
            }*/
        }

        private bool Recursion(string str, int index, int[] strAllocated, int countingSave, string Zodis, int Pakeisti)
        {
            str += Name[index];
            strAllocated[index] = 1;


            int countingFirst = 0;
            foreach (var count in strAllocated)
            {
                if (count == 0) { countingFirst++; }
            }


            /* ---------------------- */
            if ((str.Length >= 4 && countingSave == 0) || (countingSave != 0 && (str.Length) == countingSave))
            {
                foreach (var word in DictionaryList)
                {
                    int next = countingSave;
                    if (word == null) { break; }
                    if (word.Length != str.Length) { continue; }
                    //     Console.WriteLine(str + " " + word );
                    if (CheckingIfAnagram(word, str, strAllocated))
                    {

                        if (str.Equals(word))
                        {
                            FindTwoWords(Pakeisti, Zodis, str
                                , countingSave, index, strAllocated, next);
                        }
                    }
                }
            }

            /*-------------*/
            for (var j = 0; j < Name.Length; j++)
            {
                if (strAllocated[j] != 1)
                {
                    int[] MyArray1 = new int[Name.Length];
                    for (var g = 0; g < Name.Length; g++)
                    {
                        MyArray1[g] = strAllocated[g];
                    }
                    Recursion(str, j, MyArray1, countingSave, Zodis, 1);
                }
            }
            return false;
        }
        private bool FindTwoWords(int Pakeisti, string Zodis,
            string str, int countingSave, int index, int[] strAllocated, int next)
        {
            if (Pakeisti == 1 && Zodis != null && ((Zodis.Length + str.Length) == Name.Length))
            {
                //  var sentence = Zodis + " " + str;
                // Results.Add(sentence);
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
            //   Console.WriteLine();
            return false;
        }
    }
}
