using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF.DelegateTask
{
    public interface IDisplay
    {
        /* void FormattedPrint(UpperLaterDelegate _delegate, string input);*/
        void FormattedPrint(Func<string, string> _delegate, string input);
    }
}
