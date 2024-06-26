using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;
using MVC_MİMARİSİ.Models.EntityFrameWork;


namespace MVC_MİMARİSİ.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DTOMVCEntities1 db = new DTOMVCEntities1();
        public ActionResult Index()
        {
            var ogrenciler = db.TBL_OGRENCILER.ToList();
           
           
            return View(ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<System.Web.Mvc.SelectListItem> degerler = (from i in db.TBL_KULUPLER.ToList()
                                                            select new System.Web.Mvc.SelectListItem
                                                            {
                                                                Text = i.KULUPAD,
                                                                Value = i.KULUPID.ToString()
                                                            }).ToList();
            ViewBag.Degerler = degerler;

            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(TBL_OGRENCILER p3)
        {
            var klp = db.TBL_KULUPLER.Where(m => m.KULUPID == p3.TBL_KULUPLER.KULUPID).FirstOrDefault();
            p3.TBL_KULUPLER = klp;
            db.TBL_OGRENCILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            try
            {
                var ogr = db.TBL_OGRENCILER.Find(id);
                if (ogr != null)
                {
                    var notlar = db.TBL_NOTLAR.Where(n => n.OGRID == id).ToList();
                    foreach (var not in notlar)
                    {
                        db.TBL_NOTLAR.Remove(not);
                    }
                    db.TBL_OGRENCILER.Remove(ogr);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return View("Error", new HandleErrorInfo(ex, "Ogrenci", "Sil"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult OgrenciGetir(int id)
        {
            var gtrogr = db.TBL_OGRENCILER.Find(id);
           //List<System.Web.Mvc.SelectListItem> degerler = (from i in db.TBL_KULUPLER.ToList()
           //                                             select new System.Web.Mvc.SelectListItem
           //                                           {
           //                                                  Text = i.KULUPAD,
           //                                                   Value = i.KULUPID.ToString()
           //                                           }).ToList();
          //  ViewBag.Degerler = db.TBL_KULUPLER.Select(k => new System.Web.Mvc.SelectListItem
          // {
          //      Value = k.KULUPID.ToString(),
          //     Text = k.KULUPAD
          //}).ToList();


            return View("OgrenciGetir",gtrogr);
        }

        public ActionResult Guncelle(TBL_OGRENCILER p)
        {
            try
            {
                var ogr = db.TBL_OGRENCILER.Find(p.OGRIID);
              
                if (ogr == null)
                {
                    ModelState.AddModelError("", "Öğrenci bulunamadı.");
                    return View(p);
                }

                ogr.OGRAD = p.OGRAD;
                ogr.OGRSOYAD = p.OGRSOYAD;
                ogr.OGRFOTO = p.OGRFOTO;
                ogr.OGRKULUP = p.OGRKULUP;
                ogr.OGRCINSIYET = p.OGRCINSIYET;

                db.Entry(ogr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Ogrenci");
            }
            catch (Exception ex)
            {
                // Loglama
                System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
            }
            return RedirectToAction("Index", "Ogrenci");

        }
    }
}