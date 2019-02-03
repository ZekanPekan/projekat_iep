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
        public static void UpdateBidInsert(int tokens, Guid auction_id,string email, DateTime time,decimal price,string currency)
        {
            IHubContext hc = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hc.Clients.All.pageBidRefresh(tokens, email, time.ToString("MM/dd/yyyy HH:mm:ss tt"), auction_id.ToString(),price,currency);
        }

        public static void RefreshPrice(Guid auction_id, string email, int tokens, string currency, decimal price)
        {
            IHubContext hc = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hc.Clients.All.refreshPrice(auction_id, email, tokens, currency, price);
        }
    }
}