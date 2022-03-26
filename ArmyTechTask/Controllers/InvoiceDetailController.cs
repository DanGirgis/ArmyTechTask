using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArmyTechTask.Models;
using ArmyTechTask.Models.VM;

namespace ArmyTechTask.Controllers
{
    public class InvoiceDetailController : Controller
    {
        ArmyTechTaskEntities _context;
        public InvoiceDetailController()
        {
            _context = new ArmyTechTaskEntities();
        }
        // GET: InvoiceDetail
        public ActionResult Index()
        {
            var list = _context.InvoiceDetails.ToList();
            return View(list);
        }


        public ActionResult Index2()
        {
            var list = _context.InvoiceDetails.ToList();
            return View(list);
        }

        // GET: InvoiceDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var ListByid = _context.InvoiceDetails.Find(id);
            if (ListByid == null)
                HttpNotFound();
            return View(ListByid);
        }



        //public ActionResult Details2(int? id)
        //{
        //    InvoiceHeaderWithDetails2Vm invoiceHeaderWithDetails2Vm = new InvoiceHeaderWithDetails2Vm();
        //    invoiceHeaderWithDetails2Vm.Invoiceeader = this._context.InvoiceHeader.FirstOrDefault(h => h.ID == id);
        //    invoiceHeaderWithDetails2Vm.ItemsDetails = this._context.InvoiceDetails.Where(d => d.InvoiceHeaderID == id).ToList();
        //    foreach (var details in invoiceHeaderWithDetails2Vm.ItemsDetails)
        //    {
        //        invoiceHeaderWithDetails2Vm.TotalPrice += (decimal)(details.ItemCount * details.ItemPrice);
        //    }
        //    return View(invoiceHeaderWithDetails2Vm);
        //}
        // GET: InvoiceDetail/Create
        public ActionResult Create()
        {
            ViewBag.invoiceHeaders = _context.InvoiceHeader;
            return View(new InvoiceDetails { ID = 0 });
        }
        // POST: InvoiceDetail/Create
        [HttpPost]
        public ActionResult Create(InvoiceDetails _invoiceDetail)
        {
            if (!ModelState.IsValid)
                return View("Create", _invoiceDetail);
            if (_invoiceDetail.ID > 0)
                _context.Entry(_invoiceDetail).State = System.Data.Entity.EntityState.Modified;
            else
                _context.InvoiceDetails.Add(_invoiceDetail);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: InvoiceDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                HttpNotFound();
            var invoiceDetail = _context.InvoiceDetails.SingleOrDefault(i => i.ID == id);
            if (invoiceDetail == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.invoiceHeaders = _context.InvoiceHeader;
            return View("Create", invoiceDetail);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var InvoiceDetails = _context.InvoiceDetails.SingleOrDefault(l => l.ID == id);
            if (InvoiceDetails != null)
            {
                _context.InvoiceDetails.Remove(InvoiceDetails);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
