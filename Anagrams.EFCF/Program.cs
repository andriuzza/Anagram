using Anagrams.EFCF.DelegateTask;
using Anagrams.EFCF.GenericTask;
using Anagrams.EFCF.GenericTask.Enums;
using Anagrams_Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF
{
    class Program
    {
        static void Main(string[] args)
        {
            // TransferToDataBase();

            // FIRST TASK WITH GENERIC ENUMS = 
           // Console.WriteLine(GenericAndEnum.MapValueToEnum<Gender>("fdsfsd"));
            /*Action doSomething = () => {
                    Console.WriteLine("Wow");
                };

                Display ap = new Display(()=>PrintToCommandLine());
                ap.Print();*/

            // without func delegate
            /* 
             Display ap = new Display();
            ap.FormattedPrint(GetFirstUpperLetter, "labas");
             */


           /* Display ap = new Display();
            ap.FormattedPrint((arg) => GetFirstUpperLetter(arg), "labas");*/



        }

        public static void PrintToCommandLine()
        {
            Console.WriteLine("Wow");
        }
        public static void PrintToDebug()
        {
            Debug.WriteLine("Wow");
        }

        public static void PrintToFile()
        {
            using (StreamWriter writetext = File.CreateText(@"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.EFCF\DelegateTask\wow.txt"))
            {
                writetext.WriteLine("wow");
            }
        }

        public static string GetFirstUpperLetter(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static void TransferToDataBase()
        {
            FromFileToEF db = new FromFileToEF(new WordRepository());
            db.TransferToDataBase();
        }
    }
}
