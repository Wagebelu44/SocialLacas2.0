using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        
        public ActionResult SignIn()
        {
            Session["UserId"] = null;
            return View();
        }
       
        public ActionResult Services()
        {
            return View();
        }
    }
}