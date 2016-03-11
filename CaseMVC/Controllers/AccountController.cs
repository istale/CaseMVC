using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CaseMVC.Models;
using System.Collections.Generic;

namespace CaseMVC.Controllers
{
    public class AccountController : Controller
    {
        #region utility
        private List<SelectListItem> prepareLevelList(string seletedItem)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var levels = db.levels.OrderBy(a => a.l_id).ToList();

                List<SelectListItem> returnList = new List<SelectListItem>();

                seletedItem = seletedItem ?? "3";

                foreach (var item in levels)
                {
                    SelectListItem aa = new SelectListItem();
                    aa.Text = item.l_name;
                    aa.Value = item.l_value.ToString();
                    aa.Selected = seletedItem == item.l_value.ToString();

                    returnList.Add(aa);
                }

                return returnList;
            }

        }
        #endregion

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            LoginModel theItem = new LoginModel();
            theItem.acc = "";
            theItem.pwd = "";

            return View(theItem);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                LoginManager.Login(model);
                return RedirectToLocal(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            user newItem = new user();
            newItem.acc = "";
            newItem.pwd = "";
            newItem.level = 3;

            ViewBag.LevelList = this.prepareLevelList(newItem.level.ToString());

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user user)
        {
            ViewBag.LevelList = this.prepareLevelList(user.level.ToString());

            if (ModelState.IsValid)
            {
                var theUser = new user();
                theUser.acc = user.acc;
                theUser.pwd = user.pwd;
                theUser.level = user.level;

                int userId = UserAccountManager.Create(theUser);

                if (userId != 0)
                {
                    LoginModel loginModel = new LoginModel();
                    loginModel.acc = theUser.acc;
                    loginModel.pwd = theUser.pwd;

                    LoginManager.Login(loginModel);

                    return RedirectToAction("Index", "Home");
                }
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(user);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            LoginManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        #region Helper
        // 新增外部登入時用來當做 XSRF 保護
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}