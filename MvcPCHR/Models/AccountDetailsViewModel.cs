using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MvcPCHR.Models
{
    // this model will contain multiple models, since we can't pass multiple models from a controller to a single view
    // so use this one model that is made up of mulitple models to satisfy the Account Details page
    public class AccountDetailsViewModel
    {

        // the "!" is a null-forgiving operator

        public virtual PrimaryCareTbl PrimaryCare { get; set; } = null!;
        public virtual PatientTbl Patient { get; set; } = null!;



    }
}
