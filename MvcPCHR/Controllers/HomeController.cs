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

        // I could make another method with the name Index, but with parameters
        // this is bad practice, but it was a workaround
        // only thing different in this view, is the p class with login failed text
        public IActionResult LoginFailed()
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