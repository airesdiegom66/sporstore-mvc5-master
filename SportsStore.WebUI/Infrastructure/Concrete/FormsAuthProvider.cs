using SportsStore.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        //You will see a warning from Visual Studio that the FormsAuthentication.Authenticate method has been 
        //deprecated.This is part of Microsoft’s ongoing efforts to rationalize user security, which is a 
        //thorny area for any web application framework.For this chapter, the deprecated method will suffice 
        //and allow me to perform authentication using the static details I added to the Web.config file.


        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}