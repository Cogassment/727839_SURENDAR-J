using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using RegisterLoginApplication.Models;
using System.Web.Services.Description;

namespace RegisterLoginApplication.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Registration Form with Regular Expression and Required Error Message which will Store the Data Value in Database
        /// <parameter name =/*"register"*/></parameter>
        /// <summary/>
        [HttpPost]
        public ActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                if (EmailIdExists(register.EmailId) != 1)
                {
                    try
                    {
                        //Instantiate Connection String from("Constring")Web.Config
                        string connectionString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                        SqlConnection connection = new SqlConnection(connectionString);
                        //Inserting Field Values into the Table using Insert Query
                        string query = "Insert Into RegisterLoginTable Values(@FirstName,@LastName,@EmailId,@MobileNumber,@Role,@Password,@ConfirmPassword)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = register.FirstName;
                        command.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = register.LastName;
                        command.Parameters.AddWithValue("@EmailId", SqlDbType.VarChar).Value = register.EmailId;
                        command.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = register.MobileNumber;
                        command.Parameters.AddWithValue("@Role", SqlDbType.VarChar).Value = register.Role;
                        command.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = register.Password;
                        command.Parameters.AddWithValue("@ConfirmPassword", SqlDbType.VarChar).Value = register.ConfirmPassword;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        @TempData["UserMessage"] = "Registered Successfully";
                        //Clearing Entered Field Values after Submission
                        ModelState.Clear();
                        return View();
                    }
                    catch (Exception)
                    {
                        //Registration Failed Message
                        @TempData["UserMessage"] = "Registration Failed!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Email Id Already Exist";
                }
                return View();
            }
            return View();
        }

        private int EmailIdExists(string EmailId)
        {
            int isUserExists = 0;
            //Instantiate Connection String from("Constring")Web.Config
            string connectionstring = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string userExistQuery = "Select Count(*) From dbo.RegisterLoginTable Where EmailId='" + EmailId + "'";
                using (SqlCommand command = new SqlCommand(userExistQuery))
                {
                    command.Connection = connection;
                    connection.Open();
                    isUserExists = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return isUserExists;
        }
    }
}
