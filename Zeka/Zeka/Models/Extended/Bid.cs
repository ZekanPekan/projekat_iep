using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Zeka.Hubs;
using Zeka.Utils;

namespace Zeka.Models
{
    public partial class Bid
    {   
        public static Bid factoryMethod(Guid user_id,Guid auction_id,int tokens)
        {
            Bid b = new Bid();
            b.user_id = user_id;
            b.auction_id = auction_id;
            b.tokens = tokens;
            b.time = DateTime.Now;
            b.winner = false;
            return b;
        }

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

        public static Bid MaxTokensBid(List<Bid> bids)
        {
            Bid maxTokBid = null;
            int t = 0;
           
            foreach(Bid b in bids)
            {
                if (b.tokens > t) {
                    maxTokBid = b;
                    t = b.tokens;
                }
            }

            return maxTokBid;
        }

        public void saveChanges()
        {
            using (Database db = new Database())
            {
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static List<Bid> WonBids(Guid u)
        {
            using (Database db = new Database())
            {
                return db.Bid.Where(o => o.user_id == u && o.winner == true).ToList();
            }
        } 

        //TODO stub function
        public static Bid LastBidForAuction(Auction a)
        {
            //TODO use moreLinq ? ask if we can use it
            List<Bid> bids = getBidsForAuction(a.auction_id);
            if (!CollectionUtils.isEmpty(bids))
            {
                return MaxTokensBid(bids);
            }
            return null;
        }

        public static List<Bid> getBidsForAuction(Guid auction_id)
        {
            using (Database db = new Database())
            {
                return db.Bid.Where(t => t.auction_id == auction_id).Include(o=>o.User).ToList();

            }
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
                if (auction == null || auction.state != KeysUtils.AuctionOpened() || auction.token_price > this.tokens)
                    status = false;
                else { 
                    //TODO calculate more stuf? price in currency(cant think right now)
                    auction.token_price = this.tokens;
                    auction.current_price = auction.token_price * auction.tokenValue;
                    auction.saveChanges();
                    lastBid = Bid.LastBidForAuction(auction);
                    //TODO should remove last bid?
                    this.save();
                    User thisUser = User.getById(this.user_id);
                    AuctionHub.RefreshPrice(this.auction_id, thisUser.email, this.tokens, auction.currency, auction.current_price);
                    AuctionHub.UpdateBidInsert(this.tokens, this.auction_id, thisUser.first_name, thisUser.last_name, this.time);
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