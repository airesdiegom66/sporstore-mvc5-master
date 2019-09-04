using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.GetProducts);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.GetProducts
                .FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                repository.SaveProduct(product);
                //After I have saved the changes in the repository, I store a message using the TempData feature. 
                //This is a key/value dictionary similar to the session data and view bag features I used previously. 
                //The key difference from session data is that temp data is deleted at the end of the HTTP request. 
                TempData["message"] = string.Format("{0} has been saved", product.Name);

                //Notice that I return the ActionResult type from the Edit method.I have been using the ViewResult type until now.ViewResult 
                //is derived from ActionResult, and it is used when you want the framework to render a view. However, other types of ActionResults 
                //are available, and one of them is returned by the RedirectToAction method, which redirects the browser so that the Index action method is invoked
                return RedirectToAction("Index");

                //I cannot use ViewBag in this situation because the user is being redirected. 
                //ViewBag passes data between the controller and view, and it cannot hold data for longer than the current HTTP request.
                //I could have used the session data feature, but then the message would be persistent until I explicitly removed it,
                //which I would rather not have to do.So, the TempData feature is the perfect fit.The data is restricted to a single user’s
                //session(so that users do not see each other’s TempData) and will persist long enough for me to read it.I will read the data in the
                //view rendered by the action method to which I have redirected the user, which I define in the next section.

            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        //The Create method does not render its default view. Instead, it specifies that the Edit view should be used. 
        //It is perfectly acceptable for one action method to use a view that is usually associated with another view. In this case,  
        //I inject a new Product object as the view model so that the Edit view is populated with empty fields.

        public ViewResult Create()
        {
            return View("Edit", new Product());
            //i have not added a unit test for this action method.doing so would only be testing the mVC Framework ability to process the ViewResult 
            //i return as the action method result, which is something we can take for granted. (one does not usually write tests for underlying frameworks 
            //unless there is a suspicion of a problem.)
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);

            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}