using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseMVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Departments()
        {
            return View();
        }

        public ActionResult cbm(string d)
        {
            ViewBag.Level = loginUser.level;
            ViewBag.Department = d;
            return View();
        }
    }
}