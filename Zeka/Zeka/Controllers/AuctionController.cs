using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeka.Models;
using Zeka.Utils;

namespace Zeka.Controllers
{
    public class AuctionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAuction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAuction(FormCreateAuction fcAuction)
        {

            if (ModelState.IsValid)
            {
                Auction a = Auction.Create(fcAuction, ((User)Session[KeysUtils.SessionUser()]).user_id);
                a.save();
                ViewBag.Status = true;
                ViewBag.Message = "Successfuly creaed auction";
            }
            else
                ViewBag.Message = "Invalid request";

            return View(fcAuction);
        }
    }
}