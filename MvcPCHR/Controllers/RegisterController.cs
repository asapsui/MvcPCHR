using Microsoft.AspNetCore.Mvc;
using MvcPCHR.Models;

namespace MvcPCHR.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        //[HttpGet]
        //[ActionName("Signup")]
        /*
        public string Index()
        {
            return "Hello from register.index";
            //return View();
        }
        */


        public IActionResult Index()
        {
            return View();

        }

       // create a new patient object and using the user's input on the form to fill in some information
        public IActionResult Create()
        {
            PatientTbl patient = new PatientTbl();

        }
        /*
        // POST: 
        [HttpPost]
        public IActionResult Index(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Map the view model to a domain model and pass to a repository
            // Personally I use and like AutoMapper very much (http://automapper.codeplex.com)

            return RedirectToAction("Success");
         }
        */
    }
}
