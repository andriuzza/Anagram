using System;

namespace Anagrams.EFCF.DelegateTask
{
    public class DisplayWithEvents : IDisplay
    {
        public event PrintDelegate HandlingPrint;

        public void FormattedPrint(Func<string, string> _delegate, string input)
        {
            Print();
        }

        public void Print()
        {
            if(HandlingPrint != null)
            {
                HandlingPrint();
            }
        }
    }
}
