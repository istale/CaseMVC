using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseMVC.Models.Table
{
    /// <summary>
    /// 功能 (cbm頁面中的按鈕)
    /// </summary>
    public class func
    {
        public int f_id { get; set; }

        public string d_id { get; set; }

        public string name { get; set; }

        //public int name { get; set; }

        public static List<func> GetFuncList()
        {
            List<func> returnList = new List<func>();

            func a1 = new func();
            a1.f_id = 1;
            a1.d_id = "mlf";
            a1.name = "策略";
            returnList.Add(a1);

            func a2 = new func();
            a2.f_id = 2;
            a2.d_id = "m";
            a2.name = "產品管理";
            returnList.Add(a2);

            func a3 = new func();
            a3.f_id = 3;
            a3.d_id = "m";
            a3.name = "製程規劃";
            returnList.Add(a3);

            func a4 = new func();
            a4.f_id = 4;
            a4.d_id = "l";
            a4.name = "產品定價";
            returnList.Add(a4);

            func a5 = new func();
            a5.f_id = 5;
            a5.d_id = "l";
            a5.name = "配銷管理";
            returnList.Add(a5);


            func a6 = new func();
            a6.f_id = 6;
            a6.d_id = "f";
            a6.name = "財務管理";
            returnList.Add(a6);

            func a7 = new func();
            a7.f_id = 7;
            a7.d_id = "m";
            a7.name = "產品製造";
            returnList.Add(a7);

            func a8 = new func();
            a8.f_id = 8;
            a8.d_id = "m";
            a8.name = "品質管理";
            returnList.Add(a8);

            func a9 = new func();
            a9.f_id = 9;
            a9.d_id = "m";
            a9.name = "採購";
            returnList.Add(a9);

            func a10 = new func();
            a10.f_id = 10;
            a10.d_id = "l";
            a10.name = "運輸管理";
            returnList.Add(a10);

            func a11 = new func();
            a11.f_id = 11;
            a11.d_id = "l";
            a11.name = "存貨管理";
            returnList.Add(a11);

            func a12 = new func();
            a12.f_id = 12;
            a12.d_id = "l";
            a12.name = "顧客服務";
            returnList.Add(a12);

            func a13 = new func();
            a13.f_id = 13;
            a13.d_id = "f";
            a13.name = "會計總帳";
            returnList.Add(a13);

            func a14 = new func();
            a14.f_id = 14;
            a14.d_id = "f";
            a14.name = "顧客帳戶";
            returnList.Add(a14);

            return returnList;
        }
    }
}