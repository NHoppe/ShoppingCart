﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class ProductRepo : BaseRepoClass
    {
        public ProductRepo(A00964856_ShoppingCartEntities database) : base(database)
        {
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