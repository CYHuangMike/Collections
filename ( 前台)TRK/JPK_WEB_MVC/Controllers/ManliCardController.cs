using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JPK_WEB_MVC.Controllers
{
    public class ManliCardController : Controller
    {

        public ActionResult ManliCard()
        {


            return View();

        }



        //GET: ManliCard
        [HttpPost]
        public ActionResult ManliCard(string p)
        {


            int cardnum = Convert.ToInt32(Request.Form["txtNum"]);
            DBprjDataContext db = new DBprjDataContext();
            IEnumerable<ManliCard> manlicard = from t in db.ManliCard
                                               where t.cardNumber.Equals(cardnum)
                                               select t;



            //db.ManliCard.FirstOrDefault(t => t.cardNumber == cardnum);

            //if (manlicard != null)
            //{

            //}

            return View(manlicard);

        }
    }
}