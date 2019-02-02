using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeka.Utils
{
    public class CollectionUtils
    {
        public static Boolean isEmpty<T>(IEnumerable<T> list)
        {
            if (list == null || list.Count() == 0)
                return true;
            return false;
        }
    }
}