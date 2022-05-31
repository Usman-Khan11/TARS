using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARS.Models;

namespace TARS.Controllers
{
    public class HomeController : Controller
    {

        TARSEntities db = new TARSEntities();


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }


        public ActionResult Offices()
        {
            return View();
        }

        public ActionResult Center()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Center(string city, string area)
        {

            if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(area))
            {
                var data = db.tbl_center.Where(c => c.city.Contains(city) && c.area.Contains(area)).FirstOrDefault();

                if (data != null)
                {
                    var d = db.tbl_center.Where(c => c.pincode == data.pincode).ToList();
                    return View("Center", d);
                }
                else
                {
                    TempData["Err"] = "Center Not Found";
                    return View("Center");
                }


            }

            else
            {
                TempData["Err"] = "Center Not Found";
                return View("Center");
            }
            
            
        }




        public ActionResult Tracking(int? search)
        {

            if (search != 0)
            {
               var data = db.tbl_customer.Where(x => x.c_id == search).ToList();
                if (data == null)
                {
                    ViewBag.Msg = "Data Not Found";
                    return View();
                }
                else
                {
                    return View(data);

                }
            }

            else
            {
                ViewBag.Msg = "Data Not Found";
                return View();
            }
            
        }
        public ActionResult SpeedPost()
        {
            return View();
        }


        public ActionResult NormalPost()
        {
            return View();
        }


        public ActionResult VPP()
        {
            return View();
        }


        public ActionResult Privacy()
        {
            return View();
        }



    }
}