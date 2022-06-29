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

        public AccountController()
        {
        }

        // GET: AccountController
        // here we will be validating the login
        [HttpPost]
        [ActionName("Account_Details")]
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

                        // creating an instance of the PrimaryCareTbl
                        PrimaryCareTbl patientPrimaryCare = new PrimaryCareTbl();

                        // below is data for the primary care provider section

                        patientPrimaryCare.PrimaryId = (string)patientReader["Primary_Id"];
                        patientPrimaryCare.NameLast = patientReader["Name_Last"].ToString();
                        patientPrimaryCare.NameFisrt = patientReader["Name_Fisrt"].ToString();
                        patientPrimaryCare.NameLast = patientReader["Name_Last"].ToString();
                        patientPrimaryCare.Title = patientReader["Title"].ToString();
                        patientPrimaryCare.Specialty = patientReader["Specialty"].ToString();
                        patientPrimaryCare.PhoneOffice = patientReader["Phone_Office"].ToString();
                        patientPrimaryCare.PhoneMobile = patientReader["Phone_Mobile"].ToString();

                        // setting the fields of my ViewModel equal to the instances of the PatientTbl and PrimaryCareTbl
                        var viewModel = new AccountDetailsViewModel()
                        {
                            Patient = patient,
                            PrimaryCare = patientPrimaryCare
                        };

                        // use formsauthentication class to set the cookie
                        // redirect to the personal details page (to pass two routeValues, I had to use the new and brackets)

                        // instead of passing the object(s) to the Account_Details method, we can just
                        //   do that here

                        // need to handle if our patient or primarycare table are null, just return
                        return View("Account_Details", viewModel);
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
            return RedirectToAction("LoginFailed", "Home");
        }

        // GET: AccountController/Account_Details
        /*
         * if we use HttpGet it won't hide the users information from the URL, so it'll bind it
         * 
         * But if we use HttpPost it will hide this sensitive information from the URL
         */
        
        
        [HttpGet]
        /*
        public ActionResult Account_Details(PatientTbl patient, PrimaryCareTbl primaryCare)
        {
        }
        */

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
