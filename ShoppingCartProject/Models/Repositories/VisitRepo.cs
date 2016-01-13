using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class VisitRepo : BaseRepoClass
    {
        public VisitRepo(A00964856_ShoppingCartEntities db) : base(db)
        {
        }

        public void RegisterNewVisit(string sessionID, DateTime startTime)
        {
            Visit visit = new Visit();
            visit.sessionID = sessionID;
            visit.started = startTime;
            db.Visits.Add(visit);
            db.SaveChanges();
        }

        public Visit GetVisit(string sessionID)
        {
            return (from v in db.Visits
                    where v.sessionID == sessionID
                    select v).FirstOrDefault();
        }

        public void ClearVisitsOlderThan(DateTime time)
        {
            IEnumerable<Visit> visitsToDelete = from v in db.Visits
                                                from pv in v.ProductVisits
                                                where (pv.sessionID == v.sessionID && pv.updated <= time) ||
                                                        (v.ProductVisits.Count == 0 && v.started <= time)
                                                select v;
            
            foreach(Visit visit in visitsToDelete)
            {
                visit.ProductVisits.Clear();
            }

            db.Visits.RemoveRange(visitsToDelete);
            db.SaveChanges();
        }

        public void RemoveSessionID(string sessionID)
        {
            Visit visit = (from v in db.Visits
                          where v.sessionID == sessionID
                          select v).FirstOrDefault();
            if(visit != null)
            {
                db.Visits.Remove(visit);
            }
        }
    }
}