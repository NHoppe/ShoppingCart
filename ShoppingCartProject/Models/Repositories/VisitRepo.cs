using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class VisitRepo
    {
        A00964856_ShoppingCartEntities db;
        ProductVisitRepo prodVisitRepo;

        public VisitRepo(A00964856_ShoppingCartEntities db, ProductVisitRepo prodVisitRepo)
        {
            this.db = db;
            this.prodVisitRepo = prodVisitRepo;
        }

        public void RegisterNewVisit(string sessionID, DateTime startTime)
        {
            Visit visit = new Visit();
            visit.sessionID = sessionID;
            visit.started = startTime;
        }

        public void ClearVisitsOlderThan(DateTime time)
        {
            // Remove first Visits with ProductVisits
            IEnumerable<Visit> visitsToDelete = from v in db.Visits
                                                where v.ProductVisits.Any(pv => pv.sessionID == v.sessionID && pv.updated > time) ||
                                                    (v.started > time && v.ProductVisits == null)
                                                select v;

            prodVisitRepo.ClearProductVisitsOlderThan(time);
            db.Visits.RemoveRange(visitsToDelete);

            db.SaveChanges();
        }
    }
}