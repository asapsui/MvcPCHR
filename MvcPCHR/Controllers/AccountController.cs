using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using MvcPCHR.Models;

namespace MvcPCHR.Controllers
{
    public class AccountController : Controller
    {
        // using dependency injection to access the connection string
        // using dependency injection to inject my PCHRDBContext to my AccountController
        private readonly PCHRDBContext _context;

        public AccountController(PCHRDBContext context)
        {
            _context = context;
        }
        public IConfiguration Configuration { get; }

        public AccountController(IConfiguration Config)
        {
            Configuration = Config;
        }

        // GET: AccountController
        // here we will be validating the login
        [HttpPost]
        [ActionName("ValidateLogin")]
        public IActionResult Index()
        {
            // this method is validating with the database the login information the user has provided

            string username = Request.Form["username"];

            // need to hash this password 
            string password = Request.Form["password"];
            var cs = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(cs);



            string selectStatement
                = "SELECT * FROM dbo.PATIENT_TBL WHERE USERNAME=@Username AND PASSWORD = @Password;";

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
                    return RedirectToAction("Account_Details", "Account");
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
            ViewBag.ErrorMessage = "Login failed. Please try again or register below.";
            return RedirectToAction("Privacy", "Home");
        }

        
        public ActionResult Account_Details(PatientTbl patient)
        {            
            return View();
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
