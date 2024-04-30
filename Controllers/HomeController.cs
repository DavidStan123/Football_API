using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Champion_League.DBConnector;

namespace Champion_League.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //Validate the login credentials against the database
            bool loginSuccessful = ValidateLogin(username, password);

            if (loginSuccessful)
            {
                // Redirect to another page upon successful login
                return RedirectToAction("Index");
            }
            else
            {
                //Display an error message if login fails
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Signup(string newUsername, string newPassword, string email)
        {
            // Insert new user record into the database
            bool signupSuccessful = InsertUser(newUsername, newPassword, email);

            if (signupSuccessful)
            {
                // Redirect to another page upon successful signup
                return RedirectToAction("Index");
            }
            else
            {
                // Display an error message if signup fails
                ViewBag.ErrorMessage = "Failed to signup. Please try again.";
                return View("Index");
            }
        }

        // Method to validate login credentials against the database
        private bool ValidateLogin(string username, string password)
        {


            DBconnect connector = new DBconnect();
            SqlConnection connection = connector.Connect();
            // Execute SQL query to check if username and password match
            string query = "Select username and password from Users where usrname = " + username + " and password = " + password + ";";
            // Close connection and return true/false based on query result
            //connection.close();
            return true;

        }

        private bool InsertUser(string newUsername, string newPassword, string email)
        {
            DBconnect connector = new DBconnect();

            // Using statement ensures the connection is properly disposed of
            using (SqlConnection connection = connector.Connect())
            {
                // Use parameterized query to prevent SQL injection
                string query = "INSERT INTO users (username, password, email) VALUES (@Username, @Password, @Email)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", newUsername);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                //    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.WriteLine("Error inserting user: " + ex.Message);
                    
                    // Console.WriteLine("Stack Trace: " + ex.StackTrace);
                    return false;
                }
            }
        }
    }
}
