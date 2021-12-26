using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MvcPCHR.Models;
using System.Data;

namespace MvcPCHR.Controllers
{
    public class RegisterController : Controller
    {
        // using dependency injection to inject my PCHRDBContext to my RegisterController
        private readonly PCHRDBContext _context;

        public RegisterController(PCHRDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();

        }

        // create a new patient object and using the user's input on the form to fill in some information
        [HttpPost]
        public IActionResult Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //using the context instance of PCHR
            using (_context)
            {
                try
                {
                    // creating an instance of my patient model
                    PatientTbl patient = new PatientTbl();
                    patient.Username = Request.Form["Username"];
                    patient.Password = Request.Form["Password"];
                    patient.PatientId = Request.Form["PatientId"];
                    patient.FirstName = Request.Form["Firstname"];
                    patient.LastName = Request.Form["Lastname"];

                    DateTime dateInput = DateTime.Parse(Request.Form["DateOfBirth"]);
                    patient.DateOfBirth = dateInput;
                    _context.PatientTbls.Add(patient);
                    _context.SaveChanges();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
