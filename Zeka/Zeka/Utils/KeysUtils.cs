using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace Zeka.Utils
{
    public class KeysUtils
    {
        private enum SessionKeys
        {
            User,Admin
        }
        private enum AuctionStatusKeys
        {
            READY, OPENED, COMPLETED
        }

        public static String AuctionReady()
        {
            return AuctionStatusKeys.READY.ToString();
        }

        public static String AuctionOpened()
        {
            return AuctionStatusKeys.OPENED.ToString();
        }

        public static String AuctionCompleted()
        {
            return AuctionStatusKeys.COMPLETED.ToString();
        }

        public static String SessionUser()
        {
            return SessionKeys.User.ToString();
        }

        public static String SessionAdmin()
        {
            return SessionKeys.Admin.ToString();
        }
    }
}