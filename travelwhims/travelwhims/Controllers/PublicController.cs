using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using travelwhims.Models.TWdatabase;

namespace travelwhims.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public

        [HttpGet]
        public ActionResult Index()
        {
            TWDatabaseEntities db = new TWDatabaseEntities();
            var data = (from p in db.Packages
                        where p.pac_status != "inactive"
                        select p).ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult Packages()
        {
            TWDatabaseEntities db = new TWDatabaseEntities();
            var data = (from p in db.Packages
                        where p.pac_status != "inactive"
                        select p).ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult Package(int id)
        {
            TWDatabaseEntities db = new TWDatabaseEntities();
            var data = (from p in db.Packages 
                        where p.id == id 
                        select p).FirstOrDefault();

            return View(data);
        }

        public ActionResult TravelAgency(int id)
        {
            TWDatabaseEntities db = new TWDatabaseEntities();
            var data = (from ta in db.TravelAgencies
                        where ta.id == id
                        select ta).FirstOrDefault();

            return View(data);
        }

        public ActionResult TravelAgencies()
        {
            TWDatabaseEntities db = new TWDatabaseEntities();
            var data1 = (from ta in db.TravelAgencies
                         where ta.ta_rating != "notrated"
                        orderby ta.ta_rating descending
                        select ta).ToList();
            var data2 = (from ta in db.TravelAgencies
                         where ta.ta_rating == "notrated"
                         select ta).ToList();
            data1.AddRange(data2);

            return View(data1);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            TWDatabaseEntities db = new TWDatabaseEntities();
            var data= (from u in db.Users
                      where u.u_password.Equals(user.u_password) &&
                      u.u_username.Equals(user.u_username)
                      select u).FirstOrDefault();
            if(data != null)
            {
                Session["Username"] = data.u_username;
                Session["UserType"] = data.u_type;
                
                if (data.u_type == "admin")
                {
                    return RedirectToAction("Home", "Admin");
                }
                if (data.u_type == "customer")
                {
                    return RedirectToAction("Home", "Customer");
                }
                if (data.u_type == "agency")
                {
                    return RedirectToAction("Home", "Travelagency");
                }
                if (data.u_type == "manager")
                {
                    return RedirectToAction("Home", "Manager");
                }
            }

            TempData["msg"] = "Invalid Credentials";
            return RedirectToAction("Login");

        }
    }
}