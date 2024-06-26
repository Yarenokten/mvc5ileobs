using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_MİMARİSİ.Models;
using MVC_MİMARİSİ.Models.EntityFrameWork;

namespace MVC_MİMARİSİ.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DTOMVCEntities1 db=new DTOMVCEntities1();
        public ActionResult Index()
        {
            var not=db.TBL_NOTLAR.ToList();
            return View(not);
        }
        [HttpGet]
        public ActionResult YeniSinav() { 

            return View();
        }
        [HttpPost]
        public ActionResult YeniSinav(TBL_NOTLAR tbn) { 
            db.TBL_NOTLAR.Add(tbn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int id)
        {
            var guncelle = db.TBL_NOTLAR.Find(id);
            return View("NotGetir",guncelle);
        }
        public ActionResult NotSil(int id)
        {
            try
            {
                var Notlar = db.TBL_NOTLAR.Where(n => n.OGRID == id).ToList();
                foreach (var not in Notlar)
                {
                    db.TBL_NOTLAR.Remove(not);
                }
                db.SaveChanges();

                var ogrenci = db.TBL_OGRENCILER.Find(id);
                if (ogrenci != null)
                {
                    db.TBL_OGRENCILER.Remove(ogrenci);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return View("Error", new HandleErrorInfo(ex, "Ogrenci", "Sil"));
            }

            return RedirectToAction("Index","Notlar");
        }
        [HttpPost]
        public ActionResult NotGetir(Class1 model,TBL_NOTLAR p,int SINAV1=0,int SINAV2 = 0,int SINAV3 = 0,int PROJE=0)
        {
            if (model.islem == "HESAPLA")
            {
                int ORTLAMA = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ORT=ORTLAMA;
                ViewBag.DURUM = ORTLAMA >= 50 ? "Geçti" : "Kaldı";

            }
            if(model.islem == "NOTGUNCELLE")
            {
                var snv=db.TBL_NOTLAR.Find(p.NOTID);
                snv.SINAV1 = p.SINAV1;
                snv.SINAV2 = p.SINAV2;
                snv.SINAV3 = p.SINAV3;
                snv.PROJE = p.PROJE;
                snv.ORTALAMA = p.ORTALAMA;
                snv.DURUM = p.ORTALAMA >= 50;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }
           
            return View();
        }



    }
}