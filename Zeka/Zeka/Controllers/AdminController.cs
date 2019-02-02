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
        // GET: Admin
        public ActionResult Index()
        {
            List<Auction> list = Auction.getReadyAuctions();
            return View(list);
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

        [HttpPost]
        public ActionResult StartAuction(Guid key)
        {
            Auction auction = Auction.getByKey(key);
            if(auction != null)
            {
                auction.state = KeysUtils.AuctionOpened();
                auction.current_price = auction.starting_price;
                DateTime t = DateTime.Now;
                t.AddSeconds(auction.duration);
                auction.opened = DateTime.Now;
                auction.closed = t;
     
                auction.saveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }

    }
}