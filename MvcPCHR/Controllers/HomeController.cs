using Microsoft.AspNetCore.Mvc;
using MvcPCHR.Models;
using System.Diagnostics;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Web;

// this will act as our login screen

namespace MvcPCHR.Controllers
{
    public class HomeController : Controller
    {
        // using dependency injection to access the connection string
        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration Config)
        {
            Configuration = Config;
        }

        // GET: /Login/
        [HttpGet]
        //[ActionName("Home")]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [ActionName("Account")]
        public string ValidateLogin()
        {
            // this method is validating with the database the login information the user has provided

            string username = Request.Form["username"];

            // need to hash this password 
            string password = Request.Form["password"];
            var cs = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(cs);
           


            string selectStatement
                = "SELECT * FROM dbo.PATIENT_TBL WHERE USERNAME=@Username AND PASSWORD =@Password;";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();

                // associating @username and @password with parameter from the HTML form inputs 
                selectCommand.Parameters.AddWithValue("@Username", username);
                selectCommand.Parameters.AddWithValue("@Password", password);

                string userName = (string)selectCommand.ExecuteScalar();

                if (!String.IsNullOrEmpty(userName))
                {
                    // use formsauthentication class to set the cookie
                    // redirect to the personal details page
                    return "your logged in";
                   
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            // redirect to the login view, which is the home view
            return "not in database";
            
           
        }
    
        public IActionResult PersonalDetails()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}