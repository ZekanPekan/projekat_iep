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
            User
        }

        public static String SessionUser()
        {
            return SessionKeys.User.ToString();
        }
    }
}