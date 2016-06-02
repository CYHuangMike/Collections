using JPK_WEB_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gmaps.Models;

namespace JPK_WEB_MVC.Controllers
{
    public class MAPSController : Controller
    {
        DBprjDataContext db = new DBprjDataContext();
        // GET: MAPS
        public ActionResult Index()
        {
            Resort model = new Resort
            {
                Id = 1,
                Title = "大魯閣草衙道",
                Address = "806高雄市前鎮區中山四路100號",
                LogoUrl = "http://www.tarokopark.com.tw/img/logo.png",
                HomePage = "http://iiitrk.azurewebsites.net/"
            };

            return View(model);
        }

        //園區導覽
        public ActionResult AllArea()
        {
            return View();
        }

        //樓層導覽

        public ActionResult LayerInfo(string build, string layer, string companyID)
        {
            var getLayerInfo = from row in db.View_Company_CounterAll
                               where row.建築物.Equals(build) & row.樓層.Equals(layer)
                               select row;
            var getCompanyInfo = from row in db.View_Company_CounterAll
                                 where row.廠商編號.Equals(companyID) & row.樓層.Equals(layer)
                                 select row;
            if (build == "E")
            {
                ViewBag.build = "大道東";
            }
            if (build == "W")
            {
                ViewBag.build = "大道西";
            }
            if (companyID == null)
            {

                ViewBag.layerpath = "http://prjtrk.azurewebsites.net/mPic/" + build + layer + "00.jpg";
                ViewBag.layerlocation = ViewBag.build + layer + "樓";

                var model = new LayerInfoVM
                {

                    getLayerInfo = getLayerInfo,
                    getCompanyInfo = getCompanyInfo
                };
                return View(model);
            }
            else
            {

                var model = new LayerInfoVM
                {

                    getLayerInfo = getLayerInfo,
                    getCompanyInfo = getCompanyInfo
                };
                return View(model);
            }

        }
    }
}