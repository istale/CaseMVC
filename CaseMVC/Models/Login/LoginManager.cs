using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    public class LoginModel
    {
        //[Required]
        //[Display(Name = "電子郵件")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string acc { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string pwd { get; set; }
     
    }

    public static class LoginManager
    {
        public static void Login(LoginModel loginModel)
        {
            loginModel.acc = loginModel.acc.ToLower().Trim();
            //loginModel.LoginModel_Password = loginModel.LoginModel_Password.ToLower().Trim();

            var user = UserAccountManager.GetByName(loginModel.acc.ToLower());
            if (user == null)
            {
                throw new Exception("使用者不存在");
            }

            string decryptedPassword = user.pwd;
            if (decryptedPassword != loginModel.pwd)
            {
                throw new Exception("密碼錯誤");
            }

            SetLoginCookie(user);
        }

        public static void Logout()
        {
            HttpContext.Current.Response.Cookies["PassCard"].HttpOnly = true;
            HttpContext.Current.Response.Cookies["PassCard"].Expires = DateTime.Now.AddYears(-1);
        }

        //發與驗證票
        public static void SetLoginCookie(user user)
        {
            HttpCookie PassCard = new HttpCookie("PassCard");
            PassCard.HttpOnly = true;
            PassCard.Value = user.acc;
            PassCard.Expires = DateTime.Now.AddDays(1); //註解的話關掉視窗就會登出

            HttpContext.Current.Response.Cookies.Add(PassCard);
        }

        //取得驗證票的值
        public static string GetLoginCookie()
        {
            HttpCookie PassCard = HttpContext.Current.Request.Cookies["PassCard"];

            if (PassCard == null) { return ""; }

            // 回傳帳號
            return PassCard.Value;
        }
    }
}