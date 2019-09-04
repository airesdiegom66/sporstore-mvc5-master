using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        //public string Menu()
        //{
        //    return "Hello from NavController";
        //}

        //Remove the Index method that Visual Studio adds to new controllers by default and add a new action method called Menu.
        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {
            //ViewBag is a dynamic object and I create new properties simply by setting values for them.
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.GetProducts
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);


            //string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";
            //return PartialView(viewName, categories);

            return PartialView("FlexMenu", categories);
        }

        /*
         The ASP.NET MVC Framework has the concept of child actions, which are perfect for creating items such as a reusable navigation control. 
         A child action relies on the HTML helper method called Html.Action, which lets you include the output from an arbitrary action method in the current view. 
         In this case, I can create a new controller  (I will call it NavController) with an action method (which I will call Menu) that renders a navigation menu. 
         I will then use the Html.Action helper method to inject the output from that method into the layout. 
         This approach gives me a real controller that can contain whatever application logic I need and that can be unit tested like any other controller. 
         It is a nice way of creating smaller segments of an application while preserving the overall MVC Framework approach.
         */
    }
}