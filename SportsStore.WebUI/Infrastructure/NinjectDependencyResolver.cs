using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    // How to use Ninject to create a custom dependency resolver that the MVC Framework will use to instantiate objects across the application.
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {

            #region Dados fake com Mock
            //Now that I have defined an abstract interface, 
            //I could implement the persistence mechanism and hook it up to a database, 
            //but I want to add some of the other parts of the application first. In order to do this, 
            //I am going to create a mock implementation of the IProductRepository interface that will stand in until I return to the topic of data storage. 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.GetProducts).Returns(new List<Product>
            {   
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
            });

            //I want Ninject to return the same mock object whenever it gets a request for an implementation of the IProductRepository interface, which is why 
            //I used the ToConstant method to set the Ninject scope
            //Em vez de criar uma nova instância do objeto de implementação cada vez, o Ninject sempre satisfará os pedidos da interface IProductRepository com o mesmo objeto simulado.

            //Descomentar aqui
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
            #endregion
            
            //dados vindo do banco de dados
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

           //I read the value of this property using the ConfigurationManager.AppSettings property, which provides access to application settings defined in the Web.config file
           EmailSettings emailSettings = new EmailSettings {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            //Now that I have an implementation of the IOrderProcessor interface and the means to configure it, I can use Ninject to create instances of it.
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}