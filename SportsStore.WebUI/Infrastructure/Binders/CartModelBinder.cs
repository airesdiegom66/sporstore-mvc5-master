using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    //The MVC Framework uses a system called model binding to create C# objects from HTTP requests in order to pass them as parameter values to action methods. 
    //This is how the MVC framework processes forms, for example: it looks at the parameters of the action method that has been targeted and uses a model 
    //binder to get the form values sent by the browser and convert them to the type of the parameter with the same name before passing them to the action 
    //method. Model binders can create C# types from any information that is available in the request. This is one of the central features of the MVC Framework. 
    public class CartModelBinder : System.Web.Mvc.IModelBinder
    {
        private const string sessionKey = "Cart";

        /*
          The ControllerContext provides access to all the information that the controller class has, 
          which includes details of the request from the client. The ModelBindingContext gives you information about the model object 
          you are being asked to build and some tools for making the binding process easier. For my purposes, the ControllerContext class 
          is the one I am interested in. It has an HttpContext property, which in turn has a Session property that lets me get and set session data. 
          I can obtain the Cart object associated with the user's session by reading a value from the session data, and create a Cart if there 
          is not one there already. I need to tell the MVC Framework that it can use the CartModelBinder class to create instances of Cart. 
          I do this in the Application_Start method of Global.asax
        */

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //get the Cart from the session
            Cart cart = null;

            if(controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }
            //Create the Cart if there wasn't one in the session data
            if(cart == null)
            {
                cart = new Cart();

                if(controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            //return the cart
            return cart;
        }
        //There are several benefits to using a custom model binder like this. The first is that I have separated the logic used to create a Cart from
        //that of the controller, which allows me to change the way I store Cart objects without needing to change the controller.The second benefit is that
        //any controller class that works with Cart objects can simply declare them as action method parameters and take advantage of the custom model binder.
        //The third benefit, and the one I think is most important, is that I can now unit test the Cart controller without needing to mock a lot of ASP.NET plumbing.
    }
}