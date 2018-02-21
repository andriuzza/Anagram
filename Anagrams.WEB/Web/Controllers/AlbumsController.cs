using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models.Album;

namespace Web.Controllers
{
    public class AlbumsController : Controller
    {
        // GET: Albums
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAlbums()
        {
            using (var client = new HttpClient())
            {
                var task =  client.GetStringAsync("https://jsonplaceholder.typicode.com/albums");

                var result = await task;

                return View(JsonConvert.DeserializeObject<IEnumerable<Album>>(result));
            }
        }
    }
}