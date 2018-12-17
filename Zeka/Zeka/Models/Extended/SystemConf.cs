using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeka.Models
{
    public partial class SystemConf
    {
        public static SystemConf GetSystemConf()
        {
            using (Models.Database db = new Models.Database())
            {
                return db.SystemConf.FirstOrDefault();
            }
        }

        public void save()
        {
            using (Database db = new Database())
            {
                db.SystemConf.Add(this);
                db.SaveChanges();
            }
        }

    }
}