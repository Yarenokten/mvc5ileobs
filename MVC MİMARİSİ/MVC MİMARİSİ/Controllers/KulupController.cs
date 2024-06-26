using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_MİMARİSİ.Models.EntityFrameWork;
namespace MVC_MİMARİSİ.Controllers
{
    public class KulupController : Controller
    {
        // GET: Kulup
        DTOMVCEntities1 dbk = new DTOMVCEntities1();    
        public ActionResult Index()
        {
            var kulupler=dbk.TBL_KULUPLER.ToList();
            return View(kulupler);
        }
        [HttpGet]
        public ActionResult YeniKulup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKulup(TBL_KULUPLER p2)
        {
            dbk.TBL_KULUPLER.Add(p2);
            dbk.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var kulup = dbk.TBL_KULUPLER.Find(id);
            dbk.TBL_KULUPLER.Remove(kulup);
            dbk.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KulupGetir(int id) {
            var gtrklp = dbk.TBL_KULUPLER.Find(id);
            return View("KulupGetir",gtrklp);
        }
        public ActionResult KlpGuncelle(TBL_KULUPLER prmt)
        {
            var klp = dbk.TBL_KULUPLER.Find(prmt.KULUPID);
            klp.KULUPAD=prmt.KULUPAD;
            dbk.SaveChanges();
            return RedirectToAction("Index", "Kulup");
        }
    }
}