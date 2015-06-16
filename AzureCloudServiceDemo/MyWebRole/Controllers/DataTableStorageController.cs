using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MyWebRole.Common;
using MyWebRole.Models;

namespace MyWebRole.Controllers
{
    public class DataTableStorageController:Controller
    {
        public ActionResult Index()
        {
            List<CustomerEntity> customers = DataTableStorageHelper.GetData();
            return View(customers);
        }

        [HttpPost]
        public ActionResult SearchData(CustomerEntity customer)
        {
            List<CustomerEntity> customers = DataTableStorageHelper.GetRangeData(customer);
            ViewBag.SearchCustomer = customer;
            return View("Index", customers);
        }

        public ActionResult Detail(CustomerEntity customer)
        {
            var customers = DataTableStorageHelper.GetSingleData(customer);
            return View(customers);
        }
        public ActionResult Delete(CustomerEntity customer)
        {
            DataTableStorageHelper.DeleteData(customer);
            return RedirectToAction("Index", "DataTableStorage");
        }


        public ActionResult EditCustomer(CustomerEntity customer)
        {
            var customers = DataTableStorageHelper.GetSingleData(customer);
            return View(customers);            
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase imageFile, CustomerEntity customer)
        {
            if (imageFile != null)
            {
                var imageUrl = DataBlobStorageHelper.UploadFile(imageFile);
                customer.Picture = imageUrl;                
            }            
            DataTableStorageHelper.ModifyData(customer);
            return RedirectToAction("Index", "DataTableStorage");
        } 


        public ActionResult InsertData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertData(HttpPostedFileBase imageFile,CustomerEntity customer)
        {
            if (imageFile != null)
            {
                var imageUrl = DataBlobStorageHelper.UploadFile(imageFile);
                customer.Picture = imageUrl;
            }
            DataTableStorageHelper.InsertData(customer);
            return RedirectToAction("Index", "DataTableStorage");
        } 
    }
}