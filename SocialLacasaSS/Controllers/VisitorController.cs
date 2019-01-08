using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialLacasa.DataLayer;
using SocialLacasa.Models;

namespace SocialLacasa.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor
        public ActionResult Index()
        {
        
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            
            return View();
        }
        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        
        public ActionResult SignIn()
        {
            Session["UserId"] = null;
            Session["AccountFund"] = null;
            return View();
        }
       
        public ActionResult Services()
        {
            var objUser = new User();
            DataTable dtServices = objUser.GetAllServiceCategory();
            var myEnumerable = dtServices.AsEnumerable();

            List<Services> lstServices =
                (from item in myEnumerable
                 select new Services
                 {
                     Category = item.Field<string>("Category"),
                     SWserviceId = item.Field<Int32>("SWserviceId"),
                     ServiceType = item.Field<string>("ServiceType"),
                     Rate = item.Field<decimal>("Rate"),
                     MinOrder = item.Field<Int32>("MinOrder"),
                     MaxOrder = item.Field<Int32>("MaxOrder"),
                     Description = item.Field<string>("Description"),
                 }).ToList();
            return View(lstServices);
        }
    }
}