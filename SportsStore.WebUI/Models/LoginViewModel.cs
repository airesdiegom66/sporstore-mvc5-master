using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    //Given that there are only two properties, you might be tempted to do without a view model and 
    //rely on the ViewBag to pass data to the view.However, it is good practice to define view models 
    //so that the data passed from the controller to the view and from the model binder to the action 
    //method is typed consistently.

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}