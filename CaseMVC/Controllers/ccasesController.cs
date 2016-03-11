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
using System.ComponentModel.DataAnnotations;
using CaseMVC.Models.Table;

namespace CaseMVC.Controllers
{
    public class ccasesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region utility
        private List<SelectListItem> prepareLevelList(string seletedItem, int userLevel)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var levels = db.levels.OrderBy(a => a.l_id).ToList();

                List<SelectListItem> returnList = new List<SelectListItem>();

                seletedItem = seletedItem ?? "3";

                foreach (var item in levels)
                {
                    if (item.l_value <= userLevel)
                    {
                        SelectListItem aa = new SelectListItem();
                        aa.Text = item.l_name;
                        aa.Value = item.l_value.ToString();
                        aa.Selected = seletedItem == item.l_value.ToString();

                        returnList.Add(aa);
                    }
                }

                return returnList;
            }

        }
        #endregion

        /// <summary>
        /// f 為 f_id  為功能id
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public ActionResult Index(int f)
        {
            var funcList = func.GetFuncList();
            ViewBag.SearchStr = "";
            ViewBag.FuncName = funcList.FirstOrDefault(a => a.f_id == f).name;
            ViewBag.Func = f;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var caseList = db.ccases.Where(a => a.l_value <= loginUser.level && a.f_id == f).ToList();
                return View(caseList);
            }
        }

        [HttpPost]
        public ActionResult Index(int f, string freshSearch, string searchStr, IEnumerable<ccase> caseList)
        {
            var funcList = func.GetFuncList();
            ViewBag.SearchStr = searchStr;
            ViewBag.FuncName = funcList.FirstOrDefault(a => a.f_id == f).name;
            ViewBag.Func = f;

            if ("0".Equals(freshSearch))
            {
                var newList = caseList!=null ? caseList.Where(a => a.description.Contains(searchStr)).ToList() : new List<ccase>();
                return View(newList);
            }

            // 如果 freshSearch == 1
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var newList = db.ccases.Where(a => a.l_value <= loginUser.level && a.description.Contains(searchStr) && a.f_id == f).ToList();
                return View(newList);
            }

        }

        // GET: ccases/Create
        public ActionResult Create(int f)
        {
            ccase newItem = new ccase();
            newItem.description = "";
            newItem.l_value = loginUser.level;
            newItem.f_id = f;

            var funcList = func.GetFuncList();
            newItem.d_id = funcList.FirstOrDefault(a => a.f_id == f).d_id;

            ViewBag.LevelList = this.prepareLevelList(newItem.l_value.ToString(), loginUser.level);
            ViewBag.FuncName = funcList.FirstOrDefault(a => a.f_id == f).name;

            return View(newItem);
        }

        // POST: ccases/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ccase ccase)
        {

            if (ModelState.IsValid)
            {
                db.ccases.Add(ccase);
                db.SaveChanges();
                return RedirectToAction("Index", new { f = ccase.f_id});
            }

            var funcList = func.GetFuncList();
            ViewBag.LevelList = this.prepareLevelList(ccase.l_value.ToString(), loginUser.level);
            ViewBag.FuncName = funcList.FirstOrDefault(a => a.f_id == ccase.f_id).name;

            return View(ccase);
        }

        // GET: ccases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ccase ccase = db.ccases.Find(id);
            if (ccase == null)
            {
                return HttpNotFound();
            }
            var funcList = func.GetFuncList();
            ViewBag.LevelList = this.prepareLevelList(ccase.l_value.ToString(), loginUser.level);
            ViewBag.FuncName = funcList.FirstOrDefault(a => a.f_id == ccase.f_id).name;

            return View(ccase);
        }

        // POST: ccases/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ccase ccase)
        {
            ViewBag.LevelList = this.prepareLevelList(ccase.l_value.ToString(), loginUser.level);
            if (ModelState.IsValid)
            {
                db.Entry(ccase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { f = ccase.f_id});
            }
            var funcList = func.GetFuncList();
            ViewBag.LevelList = this.prepareLevelList(ccase.l_value.ToString(), loginUser.level);
            ViewBag.FuncName = funcList.FirstOrDefault(a => a.f_id == ccase.f_id).name;

            return View(ccase);
        }

        // POST: JobCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [AjaxValidateAntiForgeryTokenAttribute]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var theItem = db.ccases.FirstOrDefault(a => a.c_id == id);
                db.ccases.Remove(theItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
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
