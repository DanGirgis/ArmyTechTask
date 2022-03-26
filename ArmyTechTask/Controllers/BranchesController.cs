using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArmyTechTask.Models;

namespace ArmyTechTask.Controllers
{
    public class BranchesController : Controller
    {
        // GET: Branches
        ArmyTechTaskEntities _context;
        public BranchesController()
        {
            _context = new ArmyTechTaskEntities();
        }
        public ActionResult Index()
        {
            var list = _context.Branches.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            ViewBag.Branchlist = _context.Cities;
            return View(new Branches { ID = 0 });
        }
        [HttpPost]
        public ActionResult Create(Branches _branch)
        {
            if (!ModelState.IsValid)
                return View("Create", _branch);
            if (_branch.ID > 0)
                _context.Entry(_branch).State = System.Data.Entity.EntityState.Modified;
            else
                _context.Branches.Add(_branch);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                HttpNotFound();
            var listByid = _context.Branches.Find(id);
            if (listByid == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(listByid);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var _city = _context.Branches.SingleOrDefault(c => c.ID == id);
            ViewBag.Branchlist = _context.Cities;
            if (_city == null)
                HttpNotFound();
            return View("Create", _city);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var Branch = _context.Branches.SingleOrDefault(l => l.ID == id);
            if (Branch != null)
            {
                _context.Branches.Remove(Branch);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}