using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARS.Models;

namespace TARS.Controllers
{
    
    public class AdminController : Controller
    {

        TARSEntities db = new TARSEntities();

        // Admin Dashboard ///
        public ActionResult Dashboard()
        {

            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                int emp_count = db.tbl_employee.Count();
                TempData["emp_count"] = emp_count;

                int order_count = db.tbl_customer.Count();
                TempData["order_count"] = order_count;

                int center_count = db.tbl_customer.Count();
                TempData["center_count"] = center_count;

                TempData.Keep();
                return View();
            }
        }



        // Admin Login ///
        public ActionResult Admin_Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Admin_Login(tbl_admin a)
        {
            var data = db.tbl_admin.Where(model => model.a_email == a.a_email && model.a_password == a.a_password).FirstOrDefault();
            if (data != null)
            {
                Session["Admin_id"] = data.a_id.ToString();
                Session["Admin_name"] = data.a_name.ToString();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ModelState.Clear();
                return RedirectToAction("Admin_Login");

            }
        }


        // Admin Logout ///
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Admin_Login", "Admin");
        }




        // Add New Employee ///
        public ActionResult Add_Employee()
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                return PartialView("Add_Employee");
            }
        }


        [HttpPost]
        public ActionResult Add_Employee(tbl_employee e)
        {
            if (ModelState.IsValid == true)
            {


                string filename = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
                string extension = Path.GetExtension(e.ImageFile.FileName);

                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    filename = filename + extension;
                    e.e_picture = "~/Emp_Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Emp_Images/"), filename);
                    e.ImageFile.SaveAs(filename);

                    db.tbl_employee.Add(e);
                    int a = db.SaveChanges();

                    if (a > 0)
                    {
                        ModelState.Clear();
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        ModelState.Clear();
                        //return PartialView();
                        return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");

                    }


                }
                else
                {
                    ModelState.Clear();
                    return Content("<script language='javascript' type='text/javascript'>alert('Image must be JPG, PNG, JPEG formats');</script>");
                    
                }
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");

            }



        }


        // Admin Home ///
        public ActionResult Home()
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {

                return PartialView("Home");
            }
        }



        // Our Employee ///
        public ActionResult Our_Employee(string search)
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                var data = db.tbl_employee.ToList();


                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(p => p.e_name.ToLower().Contains(search.ToLower()) || p.city.ToLower().Contains(search.ToLower())).ToList();
                }              


                return PartialView("Our_Employee",data);
            }
        }

       

        // Delete Employee ///
        public ActionResult Delete_Employee(int id)
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {

                var deleteRow = db.tbl_employee.Where(model => model.e_id == id).FirstOrDefault();

                db.Entry(deleteRow).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Our_Employee", "Admin");
            }
            
        }




        // Parcels Coding ///
        public ActionResult Parcels(string search)
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                var data = db.tbl_customer.ToList();


                //if (!string.IsNullOrEmpty(search))
                //{
                //    data = data.Where(p => p.e_name.ToLower().Contains(search.ToLower()) || p.city.ToLower().Contains(search.ToLower())).ToList();
                //}


                return PartialView("Parcels", data);
            }
        }





        // Pricing List Coding ///
        public ActionResult Pricing()
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                var data = db.tbl_pricing.ToList();
                return PartialView("Pricing", data);
            }
        }




        // Add New Pricing Coding ///
        public ActionResult Create_Pricing(string val1, string val2, int val3)
        {

            tbl_pricing p = new tbl_pricing
            {
                City_1 = val1,
                City_2 = val2,
                Price = val3
            };

            db.tbl_pricing.Add(p);
                db.SaveChanges();
            return RedirectToAction("Pricing", "Admin");

        }



        // Delete Pricing ///
        public ActionResult Delete_Pricing(int id)
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {

                var deleteRow = db.tbl_pricing.Where(model => model.id == id).FirstOrDefault();

                db.Entry(deleteRow).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Pricing", "Admin");
            }

        }





        // Center List Coding ///
        public ActionResult Center()
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                var data = db.tbl_center.ToList();
                return PartialView("Center", data);
            }
        }


        // Add New Center Coding ///
        public ActionResult Add_Center(string val1,string val2,int val3,string val4,string val5)
        {
            tbl_center c = new tbl_center
            {
                city = val1,
                area = val5,
                address = val2,
                pincode = val3,
                contact=val4
            };

            db.tbl_center.Add(c);
            db.SaveChanges();
            return RedirectToAction("Center", "Admin");
        }


        // Delete Center ///
        public ActionResult Delete_Center(int id)
        {
            if (Session["Admin_id"] == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {

                var delete = db.tbl_center.Where(model => model.id == id).FirstOrDefault();

                db.Entry(delete).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Center", "Admin");
            }

        }






    }
}