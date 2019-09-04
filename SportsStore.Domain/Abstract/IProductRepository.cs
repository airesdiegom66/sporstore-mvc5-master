using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Abstract
{
    //Now that I have defined an abstract interface, I could implement the persistence mechanism and hook it up to a database, 
    //but I want to add some of the other parts of the application first.
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}
/*
I need some way of getting Product entities from a database.As I explained in Chapter 3, 
the model includes the persistence logic for storing and retrieving the data from the persistent data store, 
but even within the model, I want to keep a degree of separation between the data model entities and the storage and retrieval logic, 
which I achieve using the repository pattern.I will not worry about how I am going to implement data persistence for the moment, 
but I will start the process of defining an interface for it.

This interface uses IEnumerable<T> to allow a caller to obtain a sequence of Product objects, 
without saying how or where the data is stored or retrieved.A class that depends on the IProductRepository 
interface can obtain Product objects without needing to know anything about where they are coming from or how the 
implementation class will deliver them.This is the essence of the repository pattern. 
*/