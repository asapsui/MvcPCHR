using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MvcPCHR.Models;

namespace MvcPCHR.Controllers
{
    public class AccountController : Controller
    {
        // using dependency injection to access the connection string
        // using dependency injection to inject my PCHRDBContext to my AccountController
        //private readonly PCHRDBContext _context;

        /*
        public AccountController(PCHRDBContext context)
        {
            _context = context;
        }
        */
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


            SqlConnection connection = new SqlConnection(cs); //a\\pchr42563.mdf\;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30");


            /*
            string selectStatement
                = "SELECT * FROM dbo.PATIENT_TBL WHERE USERNAME=@Username AND PASSWORD=@Password";
            */

            string selectStatement
                = "SELECT * " +
                "FROM dbo.PATIENT_TBL INNER JOIN dbo.PRIMARY_CARE_TBL " +
                "On dbo.PATIENT_TBL.PRIMARY_ID = dbo.PRIMARY_CARE_TBL.PRIMARY_ID " +
                "WHERE USERNAME=@Username and PASSWORD=@Password " +
                "AND dbo.PRIMARY_CARE_TBL.PRIMARY_ID = dbo.PATIENT_TBL.PRIMARY_ID";

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
                    SqlDataReader patientReader =
                        selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    /*
                    SqlDataReader patientReade =
                            selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                    */
                    while (patientReader.Read())
                    {
                        PatientTbl patient = new PatientTbl();
                        patient.Username = username;
                        patient.Password = password;
                        patient.PatientId = (string)patientReader["Patient_Id"];
                        patient.LastName = (string)patientReader["Last_Name"];
                        patient.FirstName = (string)patientReader["First_Name"];
                        patient.DateOfBirth = (DateTime)patientReader["Date_Of_Birth"];
                        
                        // since its DBNUll we can't type cast it to a string, so we have to return the object as a string
                        // ^ doesn't make sense
                        patient.AddressStreet = patientReader["Address_Street"].ToString();
                        patient.AddressCity = patientReader["Address_City"].ToString();
                        patient.AddressState = patientReader["Address_State"].ToString();
                        patient.AddressZip = patientReader["Address_Zip"].ToString();
                        patient.PhoneHome = patientReader["Phone_Home"].ToString();
                        patient.PhoneMobile = patientReader["Phone_Mobile"].ToString();

                        // below is data for the primary care provider section


                        // use formsauthentication class to set the cookie
                        // redirect to the personal details page
                        return RedirectToAction("Account_Details", "Account", patient);
                    }
                    patientReader.Close();
                    

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

        // GET: AccountController/Account_Details
        [HttpGet]
        public ActionResult Account_Details(PatientTbl patient)
        {
            if (patient == null)
                return View(); // this view will be a page saying "No customer found"
           
            
            // need to make it to where you can only view this information, if you are authorized
            var model = new PatientTbl
            {
                Username = patient.Username,
                Password = patient.Password,
                PatientId = patient.PatientId,
                LastName = patient.LastName,
                FirstName = patient.FirstName,
                DateOfBirth = patient.DateOfBirth.Date,
                AddressStreet = patient.AddressStreet,
                AddressCity = patient.AddressCity,
                AddressState = patient.AddressState,
                AddressZip = patient.AddressZip,
                PhoneHome = patient.PhoneHome,
                PhoneMobile = patient.PhoneMobile
            };
            return View("Account_Details", model);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
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
