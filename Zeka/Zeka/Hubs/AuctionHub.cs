using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Zeka.Hubs
{
    public class AuctionHub : Hub
    {
        public static void UpdateBidInsert(int tokens, Guid auction_id,string first, string last, DateTime time)
        {
            IHubContext hc = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hc.Clients.All.pageBidRefresh(tokens, first, last, time, auction_id);
        }

        public static void RefreshPrice(Guid auction_id, string email, int tokens, string currency, decimal price)
        {
            IHubContext hc = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hc.Clients.All.refreshPrice(auction_id, email, tokens, currency, price);
        }
    }
}