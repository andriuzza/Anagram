using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Cookies
{
    public class CookiesManager :  ICookiesManager
    {
        private readonly HttpContext _request;

        public CookiesManager(HttpContext request = null)
        {   
            _request = request;
        }
        public int UpdateCookieVisitingNumber(string query)
        {
            HttpCookie howManyTimes = null;

            if (_request.Request.Cookies.Get(query) == null)
            {
                howManyTimes = new HttpCookie(query)
                {
                    Value = "1"
                };

                _request.Response.Cookies.Add(howManyTimes);

                return 1;
            }
            else
            {
                var str = howManyTimes.Value;
                int parseToInterger = Int32.Parse(str);
                parseToInterger++;

                _request.Response.Cookies[query].Value = parseToInterger.ToString();

                return 0;
            }
        }
    }
}