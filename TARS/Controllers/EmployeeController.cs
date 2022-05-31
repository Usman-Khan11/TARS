using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TARS.Models;

namespace TARS.Controllers
{
    public class EmployeeController : Controller
    {

        TARSEntities db = new TARSEntities();



        // Employee Login Code ///
        public ActionResult Emp_Login()
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                ViewBag.email = cookie["UserEmail"].ToString();

                string EncryptPassword = cookie["Password"].ToString();
                byte[] b = Convert.FromBase64String(EncryptPassword);
                string DecryptPassword = ASCIIEncoding.ASCII.GetString(b);
                ViewBag.password = DecryptPassword.ToString();
            }
            return View();
        }


        [HttpPost]
        public ActionResult Emp_Login(tbl_employee e)
        {

            var data = db.tbl_employee.Where(m => m.e_email == e.e_email && m.e_password == e.e_password).FirstOrDefault();

            if (data != null)
            {

                HttpCookie cookie = new HttpCookie("User");
                if (e.RememberMe == true)
                {

                    cookie["UserEmail"] = e.e_email;

                    byte[] b = ASCIIEncoding.ASCII.GetBytes(e.e_password);
                    string EncryptPassword = Convert.ToBase64String(b);
                    cookie["Password"] = EncryptPassword;
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    //return View();
                }


                if (data != null)
                {

                    HttpCookie emp_id = new HttpCookie("Emp_Id");
                    emp_id["Id"] = data.e_id.ToString();
                    emp_id.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(emp_id);

                    Session["Emp_id"] = data.e_id.ToString();
                    Session["Emp_name"] = data.e_name.ToString();
                    return RedirectToAction("Form", "Employee");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }


        // Main Form For Entries  ///
        public ActionResult Form()
        {
            HttpCookie Id = Request.Cookies["Emp_Id"];

            if (Id != null)
            {
                List<tbl_pricing> p1 = db.tbl_pricing.ToList();
                ViewBag.cat_1 = new SelectList(p1, "id", "City_1");
                ViewBag.cat_2 = new SelectList(p1, "id", "City_2");

                TempData.Keep();
                return View();
            }
            else
            {

                return RedirectToAction("Emp_Login", "Employee");


            }

        }

        [HttpPost]
        public ActionResult Form(tbl_customer c)
        {
            HttpCookie emp_id = Request.Cookies["Emp_Id"];

            if (ModelState.IsValid == true)
            {

                c.date = DateTime.Now;
                c.e_id = Convert.ToInt32(emp_id["Id"]);
                c.status = "Way to Destinition";
                c.total = Convert.ToInt32(TempData["price"]);
                db.tbl_customer.Add(c);
                db.SaveChanges();
                ModelState.Clear();
                TempData.Remove("price");
                return View();

            }
            else
            {
                ModelState.Clear();
                return View();
            }


        }



        // Calculate Parcel Pricing ///
        public string Find(string c1, string c2, string c3, string c4)
        {


            var data = db.tbl_pricing.Where(p => p.City_1 == c1 && p.City_2 == c2).SingleOrDefault();

            if (data != null)
            {

                int i = Convert.ToInt32(data.Price);
                if (c4 == "Speed Post" && c3 != null)
                {
                    int w = i * Convert.ToInt32(c3.ToString());
                    int s = w + 200;
                    TempData["price"] = s;
                    TempData.Keep();
                    return s.ToString();
                }
                else if (c4 == "Normal Post" && c3 != null)
                {
                    int w = i * Convert.ToInt32(c3);
                    int s = w + 100;
                    TempData["price"] = s;
                    TempData.Keep();
                    return s.ToString();
                }
                else if (c4 == "VPP" && c3 != null)
                {
                    int w = i * Convert.ToInt32(c3);
                    int s = w + 100;
                    TempData["price"] = s;
                    TempData.Keep();
                    return s.ToString();
                }
                else
                {

                    return "Error";
                }



            }
            else
            {
                return "Delivery Not Available";
            }


        }



        // Employee Profile Update ///
        public ActionResult Emp_Profile(int id)
        {
            var data = db.tbl_employee.Where(model => model.e_id == id).SingleOrDefault();
            TempData["Image"] = data.e_picture.ToString();
            return View(data);
        }

        [HttpPost]
        public ActionResult Emp_Profile(tbl_employee e)
        {
            if (e.ImageFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
                string extension = Path.GetExtension(e.ImageFile.FileName);

                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    filename = filename + extension;
                    e.e_picture = "~/Emp_Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Emp_Images/"), filename);
                    e.ImageFile.SaveAs(filename);

                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Form", "Employee");


                }
                else
                {
                    return View();
                }

            }
            else
            {
                e.e_picture = TempData["Image"].ToString();
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Form", "Employee");
            }


        }

        // Display All Parcels To Employee  ///
        public ActionResult Record()
        {
            var data = db.tbl_customer.ToList();
            return View(data);

        }


        // Update Parcels Status By Employee  ///
        public ActionResult Record_Update(int id)
        {
            var data = db.tbl_customer.Where(model => model.c_id == id).SingleOrDefault();
            return PartialView("Record_Update", data);
        }

        [HttpPost]
        public ActionResult Record_Update(tbl_customer c)
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Record", "Employee");

        }



        //Employee Logout Code  ///
        public ActionResult Logout()
        {
            HttpCookie emp_id = Request.Cookies["Emp_Id"];
            emp_id.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(emp_id);
            return RedirectToAction("Emp_Login", "Employee");
        }










    }
}