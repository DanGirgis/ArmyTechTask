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
    public class InvoiceHeaderController : Controller
    {
        ArmyTechTaskEntities _context;
        public InvoiceHeaderController()
        {
            _context = new ArmyTechTaskEntities();
        }
        // GET: InvoiceHeader
        //public ActionResult Index()
        //{
        //    var list = _context.InvoiceHeader.ToList();
        //    return View(list);
        //}


        public ActionResult Index()
        {
            List<InvoiceHeaderWithDetailsVm> invoicesVm = new List<InvoiceHeaderWithDetailsVm>();
            var list = _context.InvoiceHeader.ToList();
            foreach (var header in list)
            {
                InvoiceHeaderWithDetailsVm invoice = new InvoiceHeaderWithDetailsVm();
                invoice.InvoiceHeader = header;
                var invoices = this._context.InvoiceDetails.Where(i => i.InvoiceHeaderID == header.ID).ToList();

                List<string> itemsName = new List<string>();
                foreach (var item in invoices)
                {
                    invoice.TotalPrice += (decimal)(item.ItemCount * item.ItemPrice);
                    invoice.itemsNames += item.ItemName + " - ";
                    
                }
                invoicesVm.Add(invoice);
            }
            return View(invoicesVm);
        }
        public ActionResult Create()
        {
            ViewBag.CashierLis = _context.Cashier;
            ViewBag.BranchList = _context.Branches;
            return View(new InvoiceHeader { ID = 0 });
        }
        [HttpPost]
        public ActionResult Create(InvoiceHeader _invoiceHeader)
        {
            if (!ModelState.IsValid)
                return View(nameof(Create), _invoiceHeader);
            if (_invoiceHeader.ID > 0)
                _context.Entry(_invoiceHeader).State = System.Data.Entity.EntityState.Modified;
            else
                _context.InvoiceHeader.Add(_invoiceHeader);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var InvoiceHeaders = _context.InvoiceHeader.SingleOrDefault(l => l.ID == id);
            if (InvoiceHeaders != null)
            {
                _context.InvoiceHeader.Remove(InvoiceHeaders);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //    HttpNotFound();
            //var listByid = _context.InvoiceHeader.Find(id);
            //if (listByid == null)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //return View(listByid);
            InvoiceHeaderWithDetails2Vm invoiceHeaderWithDetails2Vm = new InvoiceHeaderWithDetails2Vm();
            invoiceHeaderWithDetails2Vm.Invoiceheader = this._context.InvoiceHeader.FirstOrDefault(h => h.ID == id);
            invoiceHeaderWithDetails2Vm.ItemsDetails = this._context.InvoiceDetails.Where(d => d.InvoiceHeaderID == id).ToList();
            foreach (var details in invoiceHeaderWithDetails2Vm.ItemsDetails)
            {
                invoiceHeaderWithDetails2Vm.TotalPrice += (decimal)(details.ItemCount * details.ItemPrice);
                
            }
            return View(invoiceHeaderWithDetails2Vm);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var Invoice = _context.InvoiceHeader.SingleOrDefault(i => i.ID == id);
            if (Invoice == null)
                HttpNotFound();
            ViewBag.CashierLis = _context.Cashier;
            ViewBag.BranchList = _context.Branches;
            return View("Create", Invoice);
        }
    }
}