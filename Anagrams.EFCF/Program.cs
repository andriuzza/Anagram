﻿using Anagrams_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF
{
    class Program
    {
        static void Main(string[] args)
        {
            TransferToDataBase();
        }
        public static void TransferToDataBase()
        {
            FromFileToEF db = new FromFileToEF(new WordRepository());
            db.TransferToDataBase();
        }
    }
}
