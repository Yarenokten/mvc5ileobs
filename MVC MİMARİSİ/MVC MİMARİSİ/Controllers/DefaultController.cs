using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_MİMARİSİ.Models.EntityFrameWork;

namespace MVC_MİMARİSİ.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DTOMVCEntities1 db = new DTOMVCEntities1();

        public ActionResult Index()
        {
            var dersler = db.TBL_DERSLER.ToList();
            return View(dersler);
        }
        [HttpGet]
        public ActionResult YeniDers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDers(TBL_DERSLER p)
        {

            db.TBL_DERSLER.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var ders=db.TBL_DERSLER.Find(id);
            db.TBL_DERSLER.Remove(ders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DersGetir(int id)
        {
            var gtrdrs = db.TBL_DERSLER.Find(id);
            return View("DersGetir",gtrdrs);
        }
        public ActionResult DrsGuncelle(TBL_DERSLER p)
        {
            var drs=db.TBL_DERSLER.Find(p.DERSID);
            drs.DERSAD=p.DERSAD;
            db.SaveChanges();
            return RedirectToAction("Index","Default");
        }
    }
}