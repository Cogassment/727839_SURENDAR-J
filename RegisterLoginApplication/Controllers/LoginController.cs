using System.Web.Mvc;
using RegisterLoginApplication.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System;

namespace RegisterLoginApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Login Page Validation with EmailId and Password from Database(RegisterLoginTable)
        /// <parameter name =/*login"*/></parameter>
        /// </summary>
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Instantiate Connection String from("Constring")Web.Config
                    string connectionString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                    SqlConnection connection = new SqlConnection(connectionString);
                    //Retreiving EmailId and Password from Table
                    string sqlquery = "select EmailId,Password from [dbo].[RegisterLoginTable] where EmailId=@EmailId and Password=@Password";
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlquery, connection);
                    command.Parameters.AddWithValue("@EmailId", login.EmailId);
                    command.Parameters.AddWithValue("@Password", login.Password);
                    SqlDataReader dataReader = command.ExecuteReader();
                    //Validating Email Id and Password
                    if (dataReader.Read())
                    {
                        TempData["EmailId"] = login.EmailId.ToString();
                        TempData["Password"] = login.Password.ToString();
                        TempData["Error"] = "";
                        return RedirectToAction("WelcomeToHomePage");
                    }
                    else
                    {
                        TempData["Error"] = "Inavlid Credentials!";
                        return RedirectToAction("Login", "Login");
                    }
                }
                catch (Exception)
                {
                    //Login Failed Message
                    @TempData["UserMessage"] = "Login Failed!";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        /// <summary>
        /// If Login Success, it Redirect to this Page: Logged In(WelocomeToHomePage)
        /// </summary>
        public ActionResult WelcomeToHomePage()
        {
            return View();
        }
    }
}




