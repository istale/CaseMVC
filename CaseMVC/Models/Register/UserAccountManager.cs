using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    public static partial class UserAccountManager
    {
        public static user GetByName(string acc)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.users.FirstOrDefault(a => a.acc.ToLower() == acc.ToLower());
            }
        }

        #region 純粹建立帳號
        public static int Create(user user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (IsUserAccountExist(user.acc))
                {
                    throw new Exception("帳號已被使用");
                }

                var userPassword = String.IsNullOrEmpty(user.pwd) ? "abc123" : user.pwd;

                user newUser = new user();
                newUser.acc = user.acc;
                newUser.pwd = user.pwd;
                newUser.level = user.level;

                db.users.Add(newUser);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    return newUser.u_id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static void Update(user userAccount)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.users.FirstOrDefault(a => a.acc == userAccount.acc);

                // 判斷是否可以修改帳號
                if (user.acc.ToLower() != userAccount.acc.ToLower())
                {
                    if (IsUserAccountExist(userAccount.acc))
                    {
                        throw new Exception("帳號已被使用");
                    }
                }

                user.acc = userAccount.acc;
                user.pwd = userAccount.pwd;
                user.level = userAccount.level;

                db.SaveChanges();
            }
        }
        #endregion

        public static bool IsUserAccountExist(string acc)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.users.Any(a => a.acc.ToLower() == acc.ToLower()))
                {
                    return true;
                }
                return false;
            }
        }
    }
}