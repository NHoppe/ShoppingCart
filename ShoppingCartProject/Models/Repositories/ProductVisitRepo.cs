using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class ProductVisitRepo : BaseRepoClass
    {
        public ProductVisitRepo(A00964856_ShoppingCartEntities database): base(database)
        {
        }

        public void ClearProductVisitsOlderThan(DateTime time)
        {
            IEnumerable <ProductVisit> pVisits = from pv in db.ProductVisits
                                                   where pv.updated <= time
                                                   select pv;
            db.ProductVisits.RemoveRange(pVisits);
            db.SaveChanges();
        }

        public void AddProductVisit(Visit visit, Product prod, int qty)
        {
            ProductVisit prodVisitData = (from pv in db.ProductVisits
                                          where pv.sessionID == visit.sessionID && pv.productID == prod.productID
                                          select pv).FirstOrDefault();
            if (prodVisitData == null)
            {
                ProductVisit prodVisit = new ProductVisit();
                prodVisit.Product = prod;
                prodVisit.Visit = visit;
                prodVisit.qtyOrdered = qty;
                prodVisit.updated = DateTime.Now;
                db.ProductVisits.Add(prodVisit);
            } else
            {
                prodVisitData.qtyOrdered = qty;
                prodVisitData.updated = DateTime.Now;
            }
            db.SaveChanges();
        }
    }
}