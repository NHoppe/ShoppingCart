using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class BaseRepoClass
    {
        public A00964856_ShoppingCartEntities db { get; private set; }

        public BaseRepoClass(A00964856_ShoppingCartEntities db)
        {
            this.db = db;
        }
    }
}