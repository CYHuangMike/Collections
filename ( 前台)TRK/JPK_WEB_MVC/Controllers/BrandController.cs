using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JPK_WEB_MVC.Controllers
{
    public class BrandController : Controller
    {
        DBprjDataContext db = new DBprjDataContext();

        [HttpGet]
        public ActionResult Search(int id)
        {
            if (id == 99) //從首頁點所傳入的參數
                return View(getAllTable());
            
            else
            {
                string keyword = Convert.ToChar(65 + id).ToString();
                return View(getTable(keyword));
            }

        }

        [HttpPost] //使用搜尋框搜尋
        public ActionResult Search(string keyword)
        {
            keyword = keyword.ToUpper();
            return View(getTable(keyword));
        }

        private IEnumerable<ViewCompanies_Shopping> getAllTable()
        {
            ViewBag.path = CDictionary.picPath;
            IEnumerable<ViewCompanies_Shopping> table = from t in db.ViewCompanies_Shopping
                                                        select t;
            return table;
        }

        private IEnumerable<ViewCompanies_Shopping> getTable(string keyword)
        {
            ViewBag.path = CDictionary.picPath;
            IEnumerable<ViewCompanies_Shopping> table = from t in db.ViewCompanies_Shopping
                                                        where t.廠商.StartsWith(keyword)
                                                        select t;
            return table;
        }
    }
}