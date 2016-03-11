using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaseMVC.Models;
using icdt.Framework;
using CaseMVC.Models.Table;

namespace CaseMVC.Controllers
{
    public class conditionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: conditions
        public ActionResult Index(string cid, string add)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ViewBag.Cid = cid;
                int intCid = Convert.ToInt32(cid);

                var theCase = db.ccases.FirstOrDefault(a => a.c_id == intCid);
                var theFunc = func.GetFuncList().FirstOrDefault(a => a.f_id == theCase.f_id);

                ViewBag.FuncName = theFunc.name;
                ViewBag.CaseDescription = theCase.description;

                List<condition> returnList = db.conditions.Where(a => a.c_id == intCid).OrderBy(a=>a.cc_id).ToList();

                if (returnList.Count == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        returnList.Add(new condition()
                        {
                            cc_id = -1,
                            c_id = intCid,
                            condition_description = ""
                        });
                    }
                }

                #region 新增10列
                List<condition> newList = new List<condition>();
                if ("1".Equals(add))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        newList.Add(new condition()
                        {
                            cc_id = -1,
                            c_id = intCid,
                            condition_description = ""
                        });
                    }
                }
                #endregion

                returnList.AddRange(newList);

                return View(returnList);
            }

        }

        // POST: conditions/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<condition> postedConditions)
        {
            int intCid = postedConditions.FirstOrDefault().c_id;
            List<condition> itemsSaveToDB = new List<condition>();

            foreach (var item in postedConditions)
            {
                if (!String.IsNullOrWhiteSpace(item.condition_description))
                {
                    itemsSaveToDB.Add(item);
                }
            }

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // 刪掉原本存在的
                var originalItems = db.conditions.Where(a => a.c_id == intCid).ToList();
                db.conditions.RemoveRange(originalItems);
                db.SaveChanges();

                // 重新插入
                db.conditions.AddRange(itemsSaveToDB);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { cid = intCid, add = 0 });
        }

        // POST: conditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [AjaxValidateAntiForgeryTokenAttribute]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id == -1)
                {
                    return RedirectToAction("Index", new { cid = id, add = 0 });
                }

                var theItem = db.conditions.FirstOrDefault(a => a.cc_id == id);
                db.conditions.Remove(theItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { cid = id, add = 0 });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
