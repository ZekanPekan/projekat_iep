using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeka.Models;

namespace Zeka.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult EditSystemConfig()
        {
            return View(SystemConf.GetSystemConf());
        }

        [HttpPost]
        public ActionResult EditSystemConfig(SystemConf conf)
        {
            if (ModelState.IsValid)
            {
                conf.save();
                ViewBag.Status = true;
                ViewBag.Message = "Successfuly edited configuration";
            }
            else
            {
                ViewBag.Message = "Invalid Request";
                conf = SystemConf.GetSystemConf();
            }
            return View(conf);
        }

    }
}