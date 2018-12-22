using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeka.Utils
{
    public class SynchronizationUtils
    {   
        private class LockWrapper
        {
            public LockWrapper()
            {
                MyLock = new object();
                Useing = 1;
            }
            public Object MyLock{ get; set; }
            public int Useing { get; set; }
        }
         
        private static Dictionary<Guid, LockWrapper> U = new Dictionary<Guid, LockWrapper>();
        private static Dictionary<Guid, LockWrapper> A = new Dictionary<Guid, LockWrapper>();

        public static object getLockOnUser(Guid user)
        {
            return getLockOn(user, U);
        }

        public static object getLockOnAuction(Guid auct)
        {
            return getLockOn(auct, A);
        }

        public static void returnLockOnUser(Guid user)
        {
            returnLockOn(user, U);
        }

        public static void returnLockOnAuction(Guid user)
        {
            returnLockOn(user, A);
        }

        private static void returnLockOn(Guid key, Dictionary<Guid, LockWrapper> dict)
        {
            lock (dict)
            {
                if(dict.TryGetValue(key,out LockWrapper value))
                {
                    if (value.Useing == 1)
                        dict.Remove(key);
                    else
                        value.Useing--;
                }
            }
        }

        private static object getLockOn(Guid key,Dictionary<Guid,LockWrapper> dict)
        {
            lock (dict)
            {
                if (!dict.TryGetValue(key, out LockWrapper value))
                {
                    value = new LockWrapper();
                    U[key] = value;
                }
                else
                {
                    value.Useing++;
                }
                return value.MyLock;
            }
        }



    }
}