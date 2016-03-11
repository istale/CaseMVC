using CaseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseMVC.Controllers
{
    public class BaseController : Controller
    {
        private user _loginUser;

        public user loginUser
        {
            get
            {
                var user = UserAccountManager.GetByName(LoginManager.GetLoginCookie());
                return user;
            }
            private set
            {
                _loginUser = value;
            }
        }
    }
}