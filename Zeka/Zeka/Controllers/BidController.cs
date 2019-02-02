using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeka.Models;
using Zeka.Utils;

namespace Zeka.Controllers
{
    public class BidController : Controller
    {
        [HttpPost]
        public ActionResult InsertBid(Guid auction_id, int token_price)
        {
            User user = (User) Session[KeysUtils.SessionUser()];
            if (user != null)
            {
                Bid bid = Bid.factoryMethod(user.user_id, auction_id, token_price + 1);
                bid.processBid();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}