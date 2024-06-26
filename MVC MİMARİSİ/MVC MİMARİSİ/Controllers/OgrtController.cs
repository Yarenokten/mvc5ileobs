using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_MİMARİSİ.Models.EntityFrameWork;

namespace MVC_MİMARİSİ.Controllers
{
    public class OgrtController : Controller
    {
        // GET: Ogrt
        DTOMVCEntities1 db=new DTOMVCEntities1();
        public ActionResult Index()
        {
            var ogrt=db.TBL_OGRT.ToList();
            return View(ogrt);
        }
    }
}