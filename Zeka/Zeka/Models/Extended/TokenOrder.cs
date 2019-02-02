using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeka.Models { 
    public partial class TokenOrder
    {
        public static List<TokenOrder> getOrdersForUser(Guid key)
        {
            using (Models.Database db = new Models.Database())
            {
                List<TokenOrder> ord = db.TokenOrder.Where(u => u.user_id == key).ToList();
                return ord;
            }
        }


    }
}