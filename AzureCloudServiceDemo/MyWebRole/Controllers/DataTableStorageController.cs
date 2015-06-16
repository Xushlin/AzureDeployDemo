using System;
using System.Collections.Generic;
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
        public ActionResult Edit(CustomerEntity customer)
        {
            DataTableStorageHelper.ModifyData(customer);
            return RedirectToAction("Index", "DataTableStorage");
        } 


        public ActionResult InsertData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertData(CustomerEntity customer)
        {
            customer.PartitionKey = Guid.NewGuid().ToString();
            customer.RowKey = "";

            DataTableStorageHelper.InsertData(customer);
            return RedirectToAction("Index", "DataTableStorage");
        } 
    }
}