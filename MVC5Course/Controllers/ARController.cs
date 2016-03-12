using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return PartialView("Index");
        }

        public ActionResult ContentTest()
        {
            return Content("<script>alert('Redriecting ...');</script>",
                "application/javascript", Encoding.UTF8);
        }

        public ActionResult FileTest()
        {
            return File(
                Server.MapPath("~/Content/Aili.jpg"),
                "image/jpeg",
                "Aili.jpg");
        }
        public ActionResult FileTest2()
        {
            return File(
                Server.MapPath("~/Content/Aili.jpg"),
                "image/jpeg");
        }

    }
}