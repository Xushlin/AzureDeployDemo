using System;
using System.Web.Mvc;
using Microsoft.ApplicationServer.Caching;
using MyCacheRole;

namespace MyWebRole.Controllers
{
    public class HomeController : Controller
    {
        public static DataCache MyCache;

        public ActionResult Index()
        {
            return View();
        }

//        public string GetCacheMessage()
//        {
//            var cacheWorkerRole=new CacheWorkerRole();
//            return cacheWorkerRole.GetCacheMessage();
//        }

        public string GetCacheMessage()
        {
            MyCache = new DataCache("default");
            MyCache.Put("MyCache", "Hello cache");
            return MyCache.Get("MyCache") as string; 
        }

        public ActionResult CallWCFService()
        {
            var client = new ServiceReference1.Service1Client();
            var model = new ServiceReference1.WCFModel(){FirstName = "Peter",LastName = "Lee"};
            return Content(client.ShowName(model));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}