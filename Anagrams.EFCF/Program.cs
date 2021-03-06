﻿using Anagrams_Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            /*//------------- HANDLING EVENTS------------ // */
           /* DisplayWithEvents ap = new DisplayWithEvents();


            ap.HandlingPrint += PrintToCommandLine;
            ap.HandlingPrint += PrintToFile;*/
        }

        public static void PrintToCommandLine()
        {
            Console.WriteLine("WowAAA");
        }
        public static void PrintToDebug()
        {
            Debug.WriteLine("Wow");
        }

        public static void PrintToFile()
        {
            using (StreamWriter writetext = File.CreateText(@"C:\Users\andrius.butkevicius\source\repos\Anagrams\Anagrams.EFCF\DelegateTask\wow.txt"))
            {
                writetext.WriteLine("wowAAA");
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
