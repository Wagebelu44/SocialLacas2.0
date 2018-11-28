using Newtonsoft.Json.Linq;
using SocialLacasa.DataLayer;
using SocialLacasa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SocialLacasa.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Addorder()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        public ActionResult SignedUpUsers()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            DataTable dtorders = new DataTable();

            var objUser = new User();
            dtorders = objUser.GetUsers();
            DataSet dsUsers = new DataSet();
            dsUsers.Tables.Add(dtorders);
            return View(dsUsers);
            
        }
        public ActionResult SupportTickets()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            var objUser = new User();
            DataTable dtTickets = objUser.GetAllTicketsForUser("0");
            var myEnumerable = dtTickets.AsEnumerable();

            List<Tickets> lstTickets =
                (from item in myEnumerable
                 select new Tickets
                 {
                     TicketId = item.Field<Int32>("TicketId"),
                     UserId = item.Field<Int32>("UserId"),
                     Subject = item.Field<string>("Subject"),
                     TicketMessage = item.Field<string>("TicketMessage"),
                     Status = item.Field<string>("Status"),
                     Date = item.Field<DateTime>("Date"),

                 }).ToList();
            return View(lstTickets);

        }
        public ActionResult PlaceOrder()
        {

            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            DataTable dtorders = new DataTable();

            var objUser = new User();
            dtorders = objUser.Getorders("0", "0");
            DataSet ds = new DataSet();
            ds.Tables.Add(dtorders);
            return View(ds);
        }
    }
}