using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SocialLacasa.DataLayer
{
    public class User
    {
        String strConnString = ConfigurationManager.ConnectionStrings["DBSocialLacasa"].ConnectionString;

        public string CheckUser(string userName, string password)
        {
            string isExist = string.Empty;
            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_GetUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                cn.Open();
                object o = cmd.ExecuteScalar();
                isExist = o.ToString();

                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return isExist;
        }

        public string checkuseraval(string userName, string email)
        {
            string isExist = string.Empty;
            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("checkUserName", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Email", email);

                cn.Open();
                object o = cmd.ExecuteScalar();
                isExist = o.ToString();

                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return isExist;
        }



        public DataTable GetAllServiceCategory()
        {

            DataTable dtservices = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetServiceCategoryWise";
            cmd.Connection = con;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                dtservices.Load(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtservices;
        }
        public DataTable BindServices(string category)
        {

            DataTable dtservices = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetAllServices";
            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = Convert.ToInt32(category);
            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtservices.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtservices;
        }
        public DataTable Getorders(string UserId, string status = "")
        {

            DataTable dtOrders = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetOrders";
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(UserId);
            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtOrders.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtOrders;
        }
        public DataTable GetAccountFunds(string UserId)
        {

            DataTable dtaccount = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetAccountFunds";

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(UserId);
            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtaccount.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtaccount;
        }

        public DataTable GetUsers()
        {

            DataTable dtUsers = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetUsers";

            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtUsers.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtUsers;
        }
        public string CheckExistingUser(string userName,  string email)
        {
            DataTable dtexisting = new DataTable();

            string isExist = "0";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand("usp_CheckExistingUser", con);
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Email", email);

            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtexisting.Load(reader);
                if (dtexisting != null && dtexisting.Rows.Count > 0)
                {
                    isExist = dtexisting.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return isExist;



        }

        public int SaveUser(string userName, string password, string email)
        {
            int newid = 0;
            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_SaveUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);

                cn.Open();
                 newid = (int)cmd.ExecuteScalar();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return newid;
        }

        public DataTable GetAllCategory()
        {

            DataTable dtCategory = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetAllCategory";


            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtCategory.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtCategory;
        }
        public void SaveFunds( decimal Amount, string userId)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_InsAccountfunds", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userId));
                cmd.Parameters.AddWithValue("@AccountFunds", Amount);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }
        public DataTable GetCharge(int serviceid)
        {
            string charge = "0.0";
            DataTable dtcharge = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_getcharge";
            cmd.Parameters.Add("@serviceid", SqlDbType.Int).Value = Convert.ToInt32(serviceid);
            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtcharge.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            //charge= dtcharge.Rows[0][0].ToString();
            return dtcharge;
        }
        public void SaveNewOrder(string category, string service, string link, string quantity, decimal charge, string userId, string orderid)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_SaveNewOrder", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Service", service);
                cmd.Parameters.AddWithValue("@Link", link);
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(quantity));
                cmd.Parameters.AddWithValue("@Charge", charge);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userId));
                cmd.Parameters.AddWithValue("@SWorderId", orderid);
                cmd.Parameters.AddWithValue("@onDate", DateTime.Now);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }

        public void SaveNewTicket(string subject, string message, string userid, string status)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_SaveTicket", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userid));
                cmd.Parameters.AddWithValue("@Subject", subject);
                cmd.Parameters.AddWithValue("@TicketMessage", message);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@onDate", DateTime.Now);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }

        public void saveTicketMessage(string message, string ticketid, bool sentbycustomer = false, string username = "")
        {

            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_SaveTicketMessage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Message", message);
                cmd.Parameters.AddWithValue("@TicketId", Convert.ToInt32(ticketid));
                cmd.Parameters.AddWithValue("@SentByCustomer", sentbycustomer);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@onDate", DateTime.Now);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }



        public DataTable GetAllTicketsForUser(string userid)
        {

            DataTable dtTickets = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetAllTicketsForUser";
            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Convert.ToInt32(userid);
            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtTickets.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtTickets;


        }


        public DataTable GetTicketConversation(string userid, string ticketid)
        {

            DataTable dtMessages = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetTicketConversation";
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(userid);
            cmd.Parameters.Add("@TicketId", SqlDbType.Int).Value = Convert.ToInt32(ticketid);

            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtMessages.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtMessages;


        }


        public string changePassword(string username, string oldpassword, string newpassword)
        {
            string rest = string.Empty;
            try
            {


                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("updatePassword", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Oldpassword", oldpassword);
                cmd.Parameters.AddWithValue("@NewPassword", newpassword);


                cn.Open();
                object res = cmd.ExecuteNonQuery();
                rest = res.ToString();

                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return rest;
        }
        public string removeUser(string userid)
        {
            string rest = string.Empty;
            try
            {


                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_removeUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userid));


                cn.Open();
                object res = cmd.ExecuteNonQuery();
                rest = res.ToString();

                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return rest;
        }
        public string removeTicket(string userid)
        {
            string rest = string.Empty;
            try
            {


                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_removeTicket", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userid));


                cn.Open();
                object res = cmd.ExecuteNonQuery();
                rest = res.ToString();

                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return rest;
        }


        public string updateDiscount(string UserId, string Discount)
        {

            string rest = string.Empty;
            try
            {


                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_updateDiscount", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(UserId));
                cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(Discount));


                cn.Open();
                object res = cmd.ExecuteNonQuery();
                rest = res.ToString();

                cn.Close();

            }
            catch (Exception ex)
            {

            }
            return rest;

        }

        public DataTable GetDiscount(string UserId)
        {

            DataTable dtdiscount = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetDiscount";

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(UserId);
            cmd.Connection = con;
            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                dtdiscount.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtdiscount;
        }


    }
}