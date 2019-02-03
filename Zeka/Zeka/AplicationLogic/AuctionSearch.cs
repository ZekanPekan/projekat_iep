using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeka.Models;
using Zeka.Utils;

namespace Zeka.AplicationLogic
{
    public class AuctionSearch
    {
        public static List<Auction> Search(int? min, int? max, int? itemsPerPage, string state, string searchString)
        {
            List<Auction> la = null;
            using (Database db = new Database())
            {
                string op_state = KeysUtils.AuctionReady();
                la = db.Auction.Where(a => !a.state.Equals(op_state)).OrderByDescending(a => a.created).ToList();
                
                if(!String.IsNullOrEmpty(state) && !state.Trim().Equals("Auction State"))
                {
                    la = la.Where(a => a.state.Equals(state.Trim())).ToList();
                }

                if (min != null)
                {
                    la = la.Where(a => a.token_price > min).ToList();
                }
                if (max != null)
                {
                    la = la.Where(a => a.token_price < max).ToList();
                }
                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    string[] tokens = searchString.ToLower().Trim().Split(' ');
                    foreach (string s in tokens)
                    {
                        la = la.Where(a => a.title.ToLower().Contains(s)).ToList();
                    }
                }
                if (itemsPerPage != null)
                {
                    la = la.Take(itemsPerPage.GetValueOrDefault()).ToList();
                }
                else
                {
                    int tt = db.SystemConf.FirstOrDefault().mrp_group;
                    la = la.Take(tt).ToList();

                }
            }

            foreach (Auction a in la)
             {
               a.checkAuctionEnd();
             }
             return la;      
        }
    }
}