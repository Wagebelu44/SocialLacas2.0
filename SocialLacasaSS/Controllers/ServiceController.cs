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
        public JsonResult PayPal(string cost)
        {
            //  string businessPaypalId = "shaheenbohra1989@gmail.com";
            string businessPaypalId = "hady-baraka777@hotmail.com";
            double itemCost = Convert.ToDouble(cost);
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            //string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";
            string redirect = "";
            redirect += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + businessPaypalId;
            redirect += "&amount=" + itemCost;
            redirect += "&item_number=1";
            redirect += "&currency_code= USD";
            redirect += "&return=" + baseUrl + "User/NewOrder";
            redirect += "&cancel_return=" + baseUrl + "User/NewOrder";
            redirect += "&notify_url=" + baseUrl + "User/NewOrder";

            return Json(redirect, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PerfectMoney()
        {


            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult WebMoney()
        {


            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult BTH()
        {


            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult APIShowStatus(int orderid)
        {
            string url = "https://socialwizards.com/api/v2?key=4319270ac33c7579f1d4f8d4c60357a5&action=status&order=" + orderid;
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
            
            //var releases = JArray.Parse(content);
            //string status = content.Substring(content.IndexOf(":") + 1, content.Length);
            //status = status.Substring(0, status.Length - 1);
            return Json(content, JsonRequestBehavior.AllowGet);
        }
        public string placeorder(int serviceid, int quantity, string link)
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
            //var releases = JArray.Parse(content);
            return content;
        }
        public JsonResult PlaceOrder_Api(int serviceid, int quantity, string link)
        {
            
            string result = placeorder(serviceid, quantity, link);
            string orderid = result.Substring(result.IndexOf(":") + 1, result.Length);
            orderid = orderid.Substring(0, orderid.Length - 1);
            //var releases = JArray.Parse(result);

            //{"order":3611783}
            return Json(orderid, JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveMassOrder(List<MassOrder> MassOrder,decimal funds)
        {
            List<string> Result = new List<string>();
            var objUser = new User();
            string issucess = "0";
            DataTable dtdetails = null;
            decimal totalCharge = 0.0M;
            foreach (var order in MassOrder)
            {
                dtdetails = objUser.GetCharge(order.ServiceId);
                string servicecharge = Convert.ToString(dtdetails.Rows[0][0]);

                order.Charge = order.Quantity * (Convert.ToDecimal(servicecharge) / 1000);
                totalCharge = totalCharge + order.Charge;
                
            }
            if (totalCharge <= funds)
            {
                foreach (var order in MassOrder)
                {
                    string result = placeorder(order.ServiceId, order.Quantity, order.Link);
                    string orderid = result.Substring(result.IndexOf(":") + 1, result.Length);
                    orderid = orderid.Substring(0, orderid.Length - 1);
                    objUser.SaveNewOrder(Convert.ToString(dtdetails.Rows[0][1]), Convert.ToString(order.ServiceId), order.Link, Convert.ToString(order.Quantity), order.Charge, Session["UserId"].ToString(),orderid);
                    issucess = "1";
                }
            }
            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveNewOrder(string category, string service, string link, string quantity, decimal charge,string orderid)
        {
            var objUser = new User();
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                objUser.SaveNewOrder(category, service, link, quantity, charge, Session["UserId"].ToString(),orderid);

                issucess = "1";
            }
            catch (Exception ex)
            {
                issucess = ex.Message.ToString();
            }

            Result.Add(issucess);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveFunds(string method, decimal Amount)
        {
            var objUser = new User();
            string issucess = "0";
            List<string> Result = new List<string>();
            try
            {
                objUser.SaveFunds(method,  Amount,  Session["UserId"].ToString());
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
                objUser.saveTicketMessage(message, ticketid, sentByCustomer, Session["UserName"].ToString());
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
            Session["isAdmin"] = "0";
            List<string> Result = new List<string>();
            try
            {
                isExist = objUser.CheckUser(userName, password);
                if (isExist != "")
                {
                    Session["UserId"] = isExist;
                    Session["UserName"] = userName;


                    if (userName == "Admin" && password == "P@ssw0rd")
                    {
                        Session["isAdmin"] = "1";
                    }
                    else
                    {
                        Session["isAdmin"] = "0";
                    }
                    try
                    {
                        DataTable dtaccount = objUser.GetAccountFunds(isExist);

                        Session["AccountFund"] = dtaccount.Rows[0][0];
                    }
                    catch {
                        Session["AccountFund"] = "0.00";
                    }
                }
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

        public JsonResult removeUser(string userid)
        {
            //string res = "0";
            var objUser = new User();
            string result = "";
            try
            {
                result = objUser.removeUser(userid);
                objUser.removeTicket(userid);

            }
            catch (Exception ex)
            {
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult checkusername(string username, string email)
        {
            string result = "0";
            var objUser = new User();
            try
            {
                result = objUser.checkuseraval(username,email);
                
            }
            catch (Exception ex)
            {
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }

}