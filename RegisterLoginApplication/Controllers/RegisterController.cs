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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Register(Register rg)
        {
               //Model Binding Validation
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Register(); Registration Success Message
                        TempData["UserMessage"] = "Registered Successfully";
                        
                        //Instantiate Connection using Connection String
                        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\RegisterLoginApplication\RegisterLoginApplication\App_Data\RegisterLoginDb.mdf;Integrated Security=True";
                        
                        //New Instance which Containing Connection String
                        SqlConnection con = new SqlConnection(constr);

                        //Inserting Field Values into the Table using Insert Query
                        string query = "INSERT INTO RegisterLoginTable VALUES(@FirstName,@LastName,@EmailId,@MobileNumber,@Role,@Password,@ConfirmPassword)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = rg.FirstName;
                        cmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = rg.LastName;
                        cmd.Parameters.AddWithValue("@EmailId", SqlDbType.VarChar).Value = rg.EmailId;
                        cmd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = rg.MobileNumber;
                        cmd.Parameters.AddWithValue("@Role", SqlDbType.VarChar).Value = rg.Role;
                        cmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = rg.Password;
                        cmd.Parameters.AddWithValue("@ConfirmPassword", SqlDbType.VarChar).Value = rg.ConfirmPassword;

                        //Open the Connection
                        con.Open();

                        //ExecuteNonQuery(); => Execute Command Statement which we given in Sql Command and Getting Query Results
                        int res = cmd.ExecuteNonQuery();

                        //Close the Connection
                        con.Close();

                        //Clearing Entered Field Values after Submission
                        ModelState.Clear();
                        return View();
                        
                    }

                    catch (Exception)
                    {
                        // Register(UsrMsg); Registration Failed Message
                        @TempData["UserMessage"] = "Registration Failed!";

                        // Email Err: Already Exist
                        @TempData["EmailError"] = "Email Id Already Exist!";
                        
                        return View();
                    }
                }
                else
                {
                    return View();
                    
                }
            
        }

    }
}

