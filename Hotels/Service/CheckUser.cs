using Hotels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotels.Service
{
    public class CheckUser
    {
        public static bool checkAvailableUser(string user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.Users.FirstOrDefault(a => a.UserName == user);
            if (data != null)
            { return true; }
            else
                return false;
        }
    }
}