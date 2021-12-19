using Microsoft.AspNetCore.Mvc;
using MvcPCHR.Models;
using System.Diagnostics;
using System.Data.SqlClient;

// this can be deleted

namespace MvcPCHR.Controllers
{
    public class PatientController : Controller
    {

       

        public IEnumerable<PatientTbl> Index()
        {
            using (var context = new PCHRDBContext())
            {

                // to get all patients
                return context.PatientTbls.ToList();
            }
        }


    }
}