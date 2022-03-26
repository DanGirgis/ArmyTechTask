using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArmyTechTask.Models;

namespace ArmyTechTask.Controllers
{
    public class CityController : Controller
    {
        ArmyTechTaskEntities _context;
        public CityController()
        {
            _context = new ArmyTechTaskEntities();
        }
        // GET: City
        public ActionResult Index()
        {
            var list = _context.Cities.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            return View(new Cities { ID = 0 });
        }
        [HttpPost]
        public ActionResult Create(Cities city)
        {
            if (!ModelState.IsValid)
                return View("Create", city);
            if (city.ID > 0)
                _context.Entry(city).State = System.Data.Entity.EntityState.Modified;
            else
                _context.Cities.Add(city);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var city = _context.Cities.SingleOrDefault(l => l.ID == id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var _city = _context.Cities.SingleOrDefault(c => c.ID == id);
            if (_city == null)
                HttpNotFound();
            return View("Create", _city);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                HttpNotFound();
            var listByid = _context.Cities.Find(id);
            if (listByid == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(listByid);
        }
    }
}