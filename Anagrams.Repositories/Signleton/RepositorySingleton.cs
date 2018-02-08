using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anagrams.Repositories.Signleton
{
    public class RepositorySingleton 
    {
        private static RepositorySingleton instance;
        public HashSet<string> data { get; private set; }

        private RepositorySingleton() { }


        public static RepositorySingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RepositorySingleton();
                }
                return instance;
            }
        }
    }
}