using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class ProductRepo
    {
        A00964856_ShoppingCartEntities db;

        public ProductRepo(A00964856_ShoppingCartEntities database)
        {
            this.db = database;
        }

        public IEnumerable<Product> GetProducts()
        {
            return from p in db.Products
                   select p;
        }

        public Product GetProduct(int id)
        {
            return (from p in db.Products
                   where p.productID == id
                   select p)
                   .FirstOrDefault();
        }
    }
}