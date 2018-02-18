using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Anagrams.Interfaces
{
    public interface ICookiesManager
    {
        int UpdateCookieVisitingNumber(string query);
    }
}
