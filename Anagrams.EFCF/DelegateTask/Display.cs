using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF.DelegateTask
{
    public delegate void PrintDelegate();
    public delegate string UpperLaterDelegate(string input);
    public class Display : IDisplay
    { 
        private PrintDelegate newDelagate = null;
        /* private UpperLaterDelegate uppperDelegate = null;
         Func<string, string> returnUpper = null;*/

        public Display(PrintDelegate task = null)
        {
            newDelagate = task;
        }

        /*With Func */
        public void FormattedPrint(Func<string,string> _delegate, string input)
        {
            Print(_delegate(input));
        }

        /*public void FormattedPrint(UpperLaterDelegate _delegate, string input)
        {
            uppperDelegate = _delegate;

            Print(uppperDelegate(input));
        }*/

        public void Print(string word)
        {
            Console.WriteLine(word);
        }

        public void Print()
        {
            newDelagate();
        }
    }
}
