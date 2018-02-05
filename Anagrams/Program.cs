using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Anagrams
{
    class Program
    {
        static void Main(string[] args)
        {
            /*First Input */
            string Name = "ilgų";

           // string configvalue1 = Properties.Settings.Default.LeastNumberOfLength;
           // string configvalue2 = Properties.Settings.DefaultMaxNumberOfResults;

            DictionaryReader nn = new DictionaryReader(new WordsTool(Name.ToLower()), Name);
            nn.ReadFile();

        }
        public static string SortName(string Name)
        {
            return Name.ToList()
                .OrderBy(s => s).ToString()
                    .ToLower();
        }

        public class DictionaryReader
        {
            public string Name { get; }
            private WordsTool _tools { get; set; }

            public DictionaryReader(WordsTool tools, string Name)
            {
                this.Name = Name;
                _tools = tools;
            }

            public void ReadFile()
            {
                string line;

                System.IO.StreamReader file =
                    new System.IO
                    .StreamReader(@"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams\zodynas.txt");

                while ((line = file.ReadLine()) != null)
                {
                    if (line[0] >= '0' && line[0] <= '9')
                    {
                        continue;
                    }

                    var words = _tools.Parsing(line);

            
                    if (_tools.CheckingIfAnagram(words.Item1))
                    {
                        Console.WriteLine(words.Item1);
                    }
                    if (_tools.CheckingIfAnagram(words.Item2))
                    {
                        Console.WriteLine(words.Item2);
                    }


                }
                file.Close();
            }
        }

        public class WordsTool
        {
            public string Name { get; private set; }

            public WordsTool(string Name)
            {
                this.Name = Name;
            }

            public Tuple<string, string> Parsing(string line)
            {
                string nameFirst = null;
                string nameSecond = null;

                int count = 0;
                for (var i = 0; i < line.Length; i++)
                {
                    char a = line[i];

                    if (a != '	')
                    {
                        nameFirst+= line[i];
                        continue;
                    }
                    count = a;
                    break;
                }

                for (var i = line.Length - 3; i >= 0; i--)
                {
                    if (line[i] != '	')
                    {
                        nameSecond+=line[i];
                        continue;
                    }
                    break;
                }

                return new Tuple<string, string>
                    (nameFirst, ReverseString(nameSecond));
            }

            public bool CheckingIfAnagram(string first)
            {
                if (first == null) return false;

                if (OrderString(Name).GetHashCode() == OrderString(first).GetHashCode())
                {
                    return true;
                }

                return false;
            }
            public string OrderString(string name)
            {
                return new string(name.OrderBy(c => c).ToArray());
            }

          private string ReverseString(string name)
           {
                if(name == null) { return null; }

                string newString = "";

                for(var i = name.Length - 1; i >= 0; i--)
                {
                    newString += name[i];
                }

                return newString;
           }

           public bool Recursion(string str, char c,  int[] strAllocated)
           {

                str += c;
                bool HightLight = false;
                for(var j = 0; j < strAllocated.Length; j++)
                {
                    if(Name[j] == c && strAllocated[j] == 0)
                    {
                        HightLight = true;
                        strAllocated[j] = 1;
                        break;
                    }
                   
                }
                if (!HightLight) { return false; }
                
                for (var i = 0; i < Name.Length - 1; i++)
                {
                    if (Name[i] == c) {  break; }

                // if LastChar == newChar // pvz priebalse ir priebalse, balse ir balse

                //  if  ChekintiArYraAnagrama();

                // if    ChekintiArIslikusiuraidziuGalima rasti

                    Recursion(str, Name[i], strAllocated);

                   

                }
              
              
           }
        }

    }
}
