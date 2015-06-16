using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationServer.Caching;

namespace MyWebRole.Controllers
{
    public class HomeController : Controller
    {
        public static DataCacheFactory myFactory;
        public static DataCache myCache;
        public static Random randomizer = new Random();

        public ActionResult Index()
        {
            return View();
        }

        public string GetCacheMessage()
        {
            if (myCache == null)
            {
                myFactory = new DataCacheFactory();
                myCache = myFactory.GetDefaultCache();
                myCache.Put("MyCache", "Welcome to use cache!");
            }

            return  myCache.Get("MyCache") as string;
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