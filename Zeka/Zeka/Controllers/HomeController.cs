using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeka.Models;
using Zeka.Utils;

namespace Zeka.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Auction> list = Auction.getAll();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormRegisterUser fruser)
        {
            if (ModelState.IsValid)
            {
                if (Models.User.EmailExists(fruser.email))
                {
                    ModelState.AddModelError("EmailExists", "Email already exists");
                }
                else
                {
                    Models.User user = Models.User.CreateFrom(fruser);
                    user.save();
                    ViewBag.Status = true;
                    ViewBag.Message = "Registration successful";
                }
            }
            else
            {
                ViewBag.Message = "Invalid request";
            }
            return View(fruser);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(FormLoginUser luser)
        {
            User  user = Models.User.getByEmail(luser.email);
            if (user == null)
            {
                ViewBag.Message = "Email does not exist";
            }
            else
            {
                if (!user.ValidatePassword(luser.password))
                {
                    ViewBag.Message = "Bad password";
                }
                else
                {
                    Session[KeysUtils.SessionUser()] = user;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(luser);
        }
    }
}