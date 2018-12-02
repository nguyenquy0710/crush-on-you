using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CoYqlp.WepApp.Infrastructure.Models;

namespace CoYqlp.WepApp.Helpers
{
    public class SYSLogHelpers
    {
        public static int WriteSYSLog(Exception exception, CoYDBConnection db, string uid, int? type = null)
        {
            db.SYSLogs.Add(new SYSLog()
            {
                DateCreated = DateTime.Now,
                LogExeption = exception.ToString(),
                Type = type,
                UserID = uid
            });
            return db.SaveChanges();
        }

        public static async Task<int> WriteSYSLogAsync(Exception exception, CoYDBConnection db, string uid, int? type = null)
        {
            return WriteSYSLog(exception, db, uid, type);
        }
    }
}