using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.SOAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var geo = new MyServiceGeo.GeoIPServiceSoapClient();

            Console.WriteLine(geo.GetGeoIP("88.119.143.44").CountryCode);
        }
        
    }

   
}
