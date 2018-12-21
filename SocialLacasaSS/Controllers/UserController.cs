using SocialLacasa.DataLayer;
using SocialLacasa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialLacasa.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        public ActionResult Account()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        public ActionResult FAQ()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        public ActionResult Orders(string status="")
        {
            if (Session["UserId"] != null)
            {
                DataTable dtorders = new DataTable();

                var objUser = new User();
                dtorders = objUser.Getorders(Session["UserId"].ToString(), status);
                DataSet ds = new DataSet();
                ds.Tables.Add(dtorders);
                return View(ds);
            }
            else
            { return RedirectToAction("SignIn", "Visitor"); }
         
        }
        public ActionResult MassOrders()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        
        public ActionResult AddFunds()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        public ActionResult Subscriptions()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            return View();
        }
        public ActionResult NewOrder(string status="")
        {
            if (Session["UserId"] != null)
            {
                var objUser = new User();
            DataTable dtCategory = objUser.GetAllCategory();
            ViewBag.CatagoryName = new SelectList(dtCategory.AsDataView(), "CatagoryId", "CatagoryName");//dtMessages.AsEnumerable().ToList();




            return View();
            }
            else
            { return RedirectToAction("SignIn", "Visitor"); }
        }

        public ActionResult Tickets() {
            if (Session["UserId"] == null)

            { return RedirectToAction("SignIn", "Visitor"); }
            var objUser = new User();
            DataTable dtTickets = objUser.GetAllTicketsForUser(Session["UserId"].ToString());
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


        public ActionResult TicketDetails(string TicketId, string isAdmin="0") {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
            var objuser = new User();
            ViewBag.isAdmin = isAdmin;
            DataTable dtMessages = objuser.GetTicketConversation(Session["UserId"].ToString(), TicketId);
            var myEnumerable = dtMessages.AsEnumerable();

            List<Messages> lstMessages =
                (from item in myEnumerable
                 select new Messages
                 {
                     Subject = item.Field<string>("Subject"),
                     TicketMessage = item.Field<string>("TicketMessage"),
                     UserName = item.Field<string>("UserName"),
                     Date = item.Field<DateTime>("Date"),
                     Message = item.Field<string>("Message"),
                     MessageDate = item.Field<DateTime?>("MessageDate"),
                     SentByCustomer = item.Field<bool>("SentByCustomer"),
                     TicketId = item.Field<int>("TicketId"),
                 }).ToList();
            return View(lstMessages);

        }

        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult Services()
        {
            if (Session["UserId"] == null)
            { return RedirectToAction("SignIn", "Visitor"); }
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
                     Rate=item.Field<decimal>("Rate"),
                     MinOrder = item.Field<Int32>("MinOrder"),
                     MaxOrder = item.Field<Int32>("MaxOrder"),
                     Description = item.Field<string>("Description"),
                     ServiceTypeRate = item.Field<string>("ServiceTypeRate"),
                 }).ToList();
            return View(lstServices);
        }
    }
}