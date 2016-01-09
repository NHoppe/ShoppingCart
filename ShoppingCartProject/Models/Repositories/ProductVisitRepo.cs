using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class ProductVisitRepo
    {
        A00964856_ShoppingCartEntities db;

        public ProductVisitRepo(A00964856_ShoppingCartEntities database)
        {
            this.db = database;
        }

        public void ClearProductVisitsOlderThan(DateTime time)
        {
            IEnumerable <ProductVisit> pVisits = from pv in db.ProductVisits
                                                   where pv.updated >= time
                                                   select pv;
            db.ProductVisits.RemoveRange(pVisits);
            db.SaveChanges();
        }
    }
}