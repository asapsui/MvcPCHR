using Microsoft.AspNetCore.Mvc;
using MvcPCHR.Models;
using System.Diagnostics;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Account")]
        public string ValidateLogin()
        {
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
                    return "This login exists";
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
            return "Wrong password or username.";
           
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