using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces
{
    public interface IServiceTool
    {
        bool CheckingIfAnagram(string word, string property, int[] array);
        string OrderString(string name);
        void SearchForAnagram();

    }
}
