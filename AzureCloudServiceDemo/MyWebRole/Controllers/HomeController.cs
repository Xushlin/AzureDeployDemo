using System;
using System.Web.Mvc;
using MyCacheRole;

namespace MyWebRole.Controllers
{
    public class HomeController : Controller
    {
        
        public static Random randomizer = new Random();

        public ActionResult Index()
        {
            return View();
        }

        public string GetCacheMessage()
        {
            var cacheWorkerRole=new CacheWorkerRole();
            return cacheWorkerRole.GetCacheMessage();
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