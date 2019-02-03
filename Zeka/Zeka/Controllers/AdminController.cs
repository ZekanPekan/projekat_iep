using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeka.Models;
using Zeka.Utils;

namespace Zeka.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            User u = (User)Session[KeysUtils.SessionAdmin()];
            if (u == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<Auction> list = Auction.getReadyAuctions();
                return View(list);
            }
        }

        [HttpGet]
        public ActionResult EditSystemConfig()
        {
            User u = (User)Session[KeysUtils.SessionAdmin()];
            if (u == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(SystemConf.GetSystemConf());
            }
        }

        [HttpPost]
        public ActionResult EditSystemConfig(SystemConf conf)
        {
            User u = (User)Session[KeysUtils.SessionAdmin()];
            if (u == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
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

        [HttpPost]
        public ActionResult StartAuction(Guid key)
        {
            User u = (User)Session[KeysUtils.SessionAdmin()];
            if (u == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Auction auction = Auction.getByKey(key);
                if (auction != null)
                {
                    auction.state = KeysUtils.AuctionOpened();
                    auction.current_price = auction.starting_price;
                    DateTime t = DateTime.Now;
                    auction.opened = t;
                    auction.closed = t.AddSeconds(auction.duration); 

                    auction.saveChanges();
                }
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}