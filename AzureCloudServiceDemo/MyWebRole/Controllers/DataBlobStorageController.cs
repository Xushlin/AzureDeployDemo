using System;
using System.Web;
using System.Web.Mvc;
using MyWebRole.Common;

namespace MyWebRole.Controllers
{
    public class DataBlobStorageController : Controller
    {
 
        public ActionResult Index()
        {      
            var blob = DataBlobStorageHelper.GetBlobs();
            return View(blob);
        }


        public ActionResult Detail(string fileUrl)
        {
            var blob = DataBlobStorageHelper.FileDetail(fileUrl);
            return View(blob);
        }


        [HttpPost]
        public ActionResult SubmitFile(HttpPostedFileBase imageFile)
        {
            DataBlobStorageHelper.UploadFile(imageFile);
            return RedirectToAction("Index", "DataBlobStorage");
        }

        public ActionResult DelFile(string fileUrl)
        {
            DataBlobStorageHelper.DeleteFile(fileUrl);
            return RedirectToAction("Index", "DataBlobStorage");
        }  
    }
}