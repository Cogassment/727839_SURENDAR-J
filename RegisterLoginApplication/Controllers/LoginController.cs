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

        [HttpPost]
        public ActionResult Login(Login lgn)
        {
               //Model Binding Validation
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Instantiate Connection using Connection String
                        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\RegisterLoginApplication\RegisterLoginApplication\App_Data\RegisterLoginDb.mdf;Integrated Security=True";
                        SqlConnection sqlcon = new SqlConnection(constr);

                        //Selecting Field Values into the Table using Select Query
                        string sqlquery = "select EmailId,Password from [dbo].[RegisterLoginTable] where EmailId=@EmailId and Password=@Password";

                        //Opent the Connection
                        sqlcon.Open();

                        //New Instance with Query & Connection
                        SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon);

                        //Adding Values
                        sqlcom.Parameters.AddWithValue("@EmailId", lgn.EmailId);
                        sqlcom.Parameters.AddWithValue("@Password", lgn.Password);
                        SqlDataReader sdr = sqlcom.ExecuteReader();
                        
                        //Validating Email Id(UserName) and Password
                        if (sdr.Read())
                        {
                            Session["EmailId"] = lgn.EmailId.ToString();
                            Session["Password"] = lgn.Password.ToString();
                            Session["Error"] = "";

                            //Successfull Login Page
                            return RedirectToAction("Welcome");
                        }
                        
                        else
                        {
                            //Error Message 
                            TempData["Error"] = "Inavlid Credentials!";

                            // Returning and Redirect to Login Page
                            return RedirectToAction("Login", "Login");
                        }
                        
                    }

                    catch (Exception e)
                    {
                        throw new Exception(e.ToString());
                    }
                }
                else
                 {
                    // Returning and Redirect to Login Page
                    return RedirectToAction("Login", "Login");
                }
               
            }
        
            // Welcome(); => Logged In Page View 
            public ActionResult Welcome()
            {
            //Return to Logged In Page
                return View();

            }

        }
    }




