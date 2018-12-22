using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeka.Utils;

namespace Zeka.Models
{
    public partial class Bid
    {
        public void save()
        {
            using (Database db = new Database())
            {
                db.Bid.Add(this);
                db.SaveChanges();
            }
        }

        public void remove()
        {
            using(Database db = new Database())
            {
                db.Bid.Remove(this);
                db.SaveChanges();
            }
        }


        //TODO stub function
        public static Bid LastBidForAuction(Guid key)
        {
            return null;
        }

        public Boolean processBid()
        {
            object u = SynchronizationUtils.getLockOnUser(this.user_id);
            Boolean status;
            lock (u)
            {
                status = User.Pay(this.tokens, this.user_id);
            }
            if (!status)
                return false;
            Bid lastBid = null;
            object a = SynchronizationUtils.getLockOnAuction(this.auction_id);
            lock (a)
            {
                Auction auction = Auction.getByKey(this.auction_id);
                if (auction == null || auction.state != KeysUtils.AuctionOpened() || auction.token_price >= this.tokens)
                    status = false;
                else { 
                    //TODO calculate more stuf? price in currency(cant think right now)
                    auction.token_price = this.tokens;
                    auction.save();
                    lastBid = Bid.LastBidForAuction(this.auction_id);
                    lastBid.remove();
                    this.save();
                    status = true;
                }
            }

            if(status == false)
            {
                lock (u)
                {
                    User.Pay(-this.tokens, this.user_id);
                }
            }
            else
            {
                if(lastBid != null)
                {
                    object uOld = SynchronizationUtils.getLockOnUser(lastBid.user_id);
                    lock (uOld)
                    {
                        User.Pay(-lastBid.tokens, lastBid.user_id);
                    }
                    SynchronizationUtils.returnLockOnUser(lastBid.user_id);
                }

            }
            SynchronizationUtils.returnLockOnUser(this.user_id);
            SynchronizationUtils.returnLockOnAuction(this.auction_id);
            return status;
        }
    }
}