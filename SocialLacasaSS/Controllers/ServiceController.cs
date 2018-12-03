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
    public class ServiceController : Controller
    {

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult logOut()
        {
            Session["isAdmin"] = null;
            Session["UserId"] = null;
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult PayPal()
        {
            string businessPaypalId = "shaheenbohra1989@gmail.com";
            double itemCost = 10.00;
            string baseUrl=Request.Url.GetLeftPart(UriPartial.Authority);
            //string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";
            string redirect = "";
            redirect += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + businessPaypalId;
            redirect += "&amount=" + itemCost;
            redirect += "&item_number=1";
            redirect += "&currency_code= USD";
            redirect += "&return=" + baseUrl + "User/NewOrder";
            redirect += "&cancel_return=" + baseUrl + "User/NewOrder";
            redirect += "&notify_url=" + baseUrl + "User/NewOrder";
            
            return Json(redirect, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PlaceOrder_Api(int serviceid, int quantity, string link)
        {
            string url = "https://socialwizards.com/api/v2?key=4319270ac33c7579f1d4f8d4c60357a5&action=add&service=" + serviceid + "&link=" + link + "&quantity=" + quantity;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }
            var releases = JArray.Parse(content);
            return Json(releases, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindServices(string category)
        {
            var objUser = new User();
            DataTable dtServices = objUser.BindServices(category);

            var myEnumerable = dtServices.AsEnumerable();

            List<Services> lstServices =
                (from item in myEnumerable
                 select new Services
                 {
                     SWserviceId = item.Field<Int32>("SWserviceId"),
                     ServiceType = item.Field<string>("ServiceType"),
                     Description = item.Field<string>("Description"),
                     Rate = item.Field<decimal>("Rate")

                 }).ToList();
            return Json(lstServices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNewOrder(string category, string service, string link, string quantity, decimal charge)
        {
            var objUser = new User();
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                objUser.SaveNewOrder(category, service, link, quantity, charge, Session["UserId"].ToString());
                issucess = "1";
            }
            catch (Exception ex)
            {
                issucess = ex.Message.ToString();
            }

            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveFunds(string method, string AccountName, string AccountNumber, string Cvv, string expiry, decimal Amount)
        {
            var objUser = new User();
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                objUser.SaveFunds(method, AccountName, AccountNumber, Cvv, Amount, expiry, Session["UserId"].ToString());
                issucess = "1";
            }
            catch (Exception ex)
            {
                issucess = ex.Message.ToString();
            }

            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubmitTicket(string subject, string ticketmessage, string status = "closed")
        {
            var objUser = new User();
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                objUser.SaveNewTicket(subject, ticketmessage, Session["UserId"].ToString(), status);
                issucess = "1";
            }
            catch (Exception ex)
            {
                issucess = ex.Message.ToString();
            }

            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult saveTicketMessage(string message, string ticketid, string isAdmin)
        {
            var objUser = new User();
            bool sentByCustomer = false;
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                if (isAdmin == "0")
                    sentByCustomer = true;
                else
                    sentByCustomer = false;
                objUser.saveTicketMessage(message, ticketid, sentByCustomer,Session["UserName"].ToString());
                issucess = "1";
            }
            catch (Exception ex)
            {
                issucess = ex.Message.ToString();
            }

            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }




        //Action called when user is registered
        public JsonResult SaveUser(string userName, string password, string email)
        {
            var objUser = new User();
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                objUser.SaveUser(userName, password, email);
                issucess = "1";
            }
            catch (Exception ex)
            {
                issucess = ex.Message.ToString();
            }

            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckUser(string userName, string password)
        {
            var objUser = new User();
            string isExist = "0";
            List<string> Result = new List<string>();
            try
            {
                isExist = objUser.CheckUser(userName, password);
                if (userName == "Admin")
                {
                    Session["isAdmin"] = "1";
                }
                else
                {
                    Session["isAdmin"] = "0";
                }
                Session["UserId"] = isExist;
                Session["UserName"] = userName;
            }
            catch (Exception ex)
            {
                isExist = ex.Message.ToString();
            }

            Result.Add(isExist);
            Result.Add(Session["isAdmin"].ToString());
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult changePassword(string username, string oldpassword, string newpassword)
        {
            string res = "0";
            var objUser = new User();
            List<string> Result = new List<string>();
            try
            {
                res = objUser.changePassword(username, oldpassword, newpassword);
            }
            catch (Exception ex)
            {
            }

            Result.Add(res);
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

    }

}