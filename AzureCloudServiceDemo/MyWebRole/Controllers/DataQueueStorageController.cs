using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MyWebRole.Common;
using MyWebRole.Models;

namespace MyWebRole.Controllers
{
    public class DataQueueStorageController : Controller
    {
        public ActionResult Index()
        {
            var queue = DataQueueStorageHelper.GetMessage();
            return View(queue);
        }

        public ActionResult PeekQueue()
        {
           var peek = DataQueueStorageHelper.PeekQueue();
            return RedirectToAction("Index", "DataQueueStorage");
        }

        public ActionResult ChangeMessageContent()
        {
            DataQueueStorageHelper.ChangeMessageContent();
            return RedirectToAction("Index", "DataQueueStorage");
        }

        public ActionResult DeQueueMessage()
        {
           DataQueueStorageHelper.DeQueueMessage();
           return RedirectToAction("Index", "DataQueueStorage");
        }

        public ActionResult DeleteQueue()
        {
            DataQueueStorageHelper.DeleteQueue();
            return RedirectToAction("Index", "DataQueueStorage");
        }
    }
}