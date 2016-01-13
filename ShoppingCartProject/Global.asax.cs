using ShoppingCartProject.BusinessLogic;
using ShoppingCartProject.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingCartProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionHelper sessionHlp = new SessionHelper();

            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();

            VisitRepo visitRepo = new VisitRepo(db);
            visitRepo.ClearVisitsOlderThan(sessionHlp.Expired);
            visitRepo.RemoveSessionID(sessionHlp.SessionID);
            visitRepo.RegisterNewVisit(sessionHlp.SessionID, sessionHlp.Start);
        }
    }
}
