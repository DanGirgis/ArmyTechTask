using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArmyTechTask.Models;
namespace ArmyTechTask.Controllers
{
    public class CashierController : Controller
    {
        ArmyTechTaskEntities _context;
        public CashierController()
        {
            _context = new ArmyTechTaskEntities();
        }
        // GET: Cashier
        public ActionResult Index()
        {
            var list = _context.Cashier.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            ViewBag.BranchList = _context.Branches;
            return View(new Cashier { ID = 0 });
        }
        [HttpPost]
        public ActionResult Create(Cashier _cashier)
        {
            if (!ModelState.IsValid)
                return View("Create", _cashier);
            if (_cashier.ID > 0)
                _context.Entry(_cashier).State = System.Data.Entity.EntityState.Modified;
            else
                _context.Cashier.Add(_cashier);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var Cashier = _context.Cashier.SingleOrDefault(c => c.ID == id);
            if (Cashier == null)
                HttpNotFound();
            ViewBag.BranchList = _context.Branches;
            return View("Create", Cashier);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                HttpNotFound();
            var listByid = _context.Cashier.Find(id);
            if (listByid == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(listByid);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var Cashier = _context.Cashier.SingleOrDefault(l => l.ID == id);
            if (Cashier != null)
            {
                _context.Cashier.Remove(Cashier);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}