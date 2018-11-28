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

        public void SaveUser(string userName, string password, string email)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_SaveUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
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
        public void SaveFunds(string method, string AccountName, string AccountNumber, string Cvv, decimal Amount, string expiry, string userId)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand("usp_InsAccountfunds", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userId));
                cmd.Parameters.AddWithValue("@AccountFunds", Amount);
                cmd.Parameters.AddWithValue("@UserAccountNumber", AccountNumber);
                cmd.Parameters.AddWithValue("@UserCvv", Convert.ToInt32(Cvv));
                cmd.Parameters.AddWithValue("@ExpiryDate", expiry);
                cmd.Parameters.AddWithValue("@AccountName", AccountName);
                cmd.Parameters.AddWithValue("@Method", method);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }
        public void SaveNewOrder(string category, string service, string link, string quantity, decimal charge, string userId)
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

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }

        public void saveTicketMessage(string message, string ticketid, bool sentbycustomer = false,string username="")
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





    }
}