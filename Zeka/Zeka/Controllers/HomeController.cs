using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeka.AplicationLogic;
using Zeka.Models;
using Zeka.Utils;

namespace Zeka.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Auction> list = Auction.getAll();
            foreach(Auction a in list)
            {
                if(a.closed != null)
                {
                    a.duration = (int)(a.closed.GetValueOrDefault() - DateTime.Now).TotalSeconds;
                }
            }
            return View(list);
        }

        public ActionResult IndexSearched(int? minimum, int? maximum, int? itemsperpage, string state, string searchstring)
        {
            List<Auction> list = AuctionSearch.Search(minimum, maximum, itemsperpage, state, searchstring);
            foreach (Auction a in list)
            {
                if (a.closed != null)
                {
                    a.duration = (int)(a.closed.GetValueOrDefault() - DateTime.Now).TotalSeconds;
                }
            }
            return View(list);
        }


        [HttpGet]
        public ActionResult ChangeProfile()
        {
            User u = (User)Session[KeysUtils.SessionUser()];
            if(u == null)
            {
                return RedirectToAction("Register", "Home");
            }
            else
            {
                FormChangeUser fcu = new FormChangeUser();
                fcu.lastName = u.last_name.Trim();
                fcu.firstName = u.first_name.Trim();
                fcu.email = u.email;
                fcu.password = "";

                return View(fcu);
            }
        }

        public ActionResult SignOut()
        {
            Session[KeysUtils.SessionUser()] = null;
            Session[KeysUtils.SessionAdmin()] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyOrders()
        {
            User u = (User)Session[KeysUtils.SessionUser()];
            if (u == null)
                return RedirectToAction("Index", "Home");
            else
            {
                List<TokenOrder> to = TokenOrder.getOrdersForUser(u.user_id);
                return View(to);
            }
        }

        [HttpGet]
        public ActionResult MyWinns()
        {
            User u = (User)Session[KeysUtils.SessionUser()];
            if(u == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<Bid> ll = Bid.WonBids(u.user_id);
                List<Auction> la = new List<Auction>();
                foreach(Bid b in ll)
                {
                    la.Add(Auction.getByKey(b.auction_id));
                }
                return View(la);

            }
        }

        [HttpGet]
        public ActionResult MyTokens()
        {
            User u = (User)Session[KeysUtils.SessionUser()];
            if(u == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                u = Models.User.getById(u.user_id);
                ViewBag.tokens = u.tokens;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangeProfile(FormChangeUser u)
        {
            User user = (User)Session[KeysUtils.SessionUser()];
            if (!user.email.Trim().Equals(u.email) &&  Models.User.EmailExists(u.email))
            {
                ModelState.AddModelError("EmailExists", "Email already exists");
            }
            else
            {
                if (u.password == null || u.password.Trim().Equals(""))
                    u.password = ((User)(Session[KeysUtils.SessionUser()])).password;
                else
                {
                    user.password = CryptUtils.Hash(u.password);
                }
                user.email = u.email;
                user.first_name = u.firstName;
                user.last_name = u.lastName;
                user.saveChanges();
                Session[KeysUtils.SessionUser()] = user;
                ViewBag.Status = true;
                ViewBag.Message = "Changed profile  successfully";

            }



            return View(u);
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
                    if (user.admin_flag)
                    {
                        Session[KeysUtils.SessionAdmin()] = user;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Session[KeysUtils.SessionUser()] = user;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(luser);
        }
    }
}