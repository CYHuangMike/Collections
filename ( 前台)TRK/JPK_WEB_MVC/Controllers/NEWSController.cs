using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JPK_WEB_MVC.Controllers
{
    public class NEWSController : Controller
    {
        // GET: 全部最新消息
        public ActionResult News()
        {
            ViewBag.path = CDictionary.picPath;

            DBprjDataContext db = new DBprjDataContext();
            IEnumerable<ViewNews> table = from t in db.ViewNews
                                         select t;
            return View(table);

        }

        public ActionResult Promotions()
        {
            ViewBag.path = CDictionary.picPath;

            DBprjDataContext db = new DBprjDataContext();
            IEnumerable<ViewPromotions> table = from t in db.ViewPromotions
                                               select t;
            return View(table);
        }

        public ActionResult CompanyPromotion(string id)
        {
            ViewBag.path = CDictionary.picPath;
            DBprjDataContext db = new DBprjDataContext();
            IEnumerable<ViewPromotions> table = from t in db.ViewPromotions
                                                where t.廠商.StartsWith(id)
                                                select t;

            return View(table);
        }

        public ActionResult dialog()
        {
            return View();
        }


    }
}