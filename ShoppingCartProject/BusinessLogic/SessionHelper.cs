using ShoppingCartProject.BusinessLogic.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.BusinessLogic
{
    public class SessionHelper
    {
        public const string SESSION_START = "Session_Start";
        public const string SESSION_PRODUCTS = "Session_Products";

        public string SessionID
        {
            get
            {
                if (HttpContext.Current.Session.SessionID != null)
                    return HttpContext.Current.Session.SessionID;
                return null;
            }
        }

        public void Initialize()
        {
            HttpContext.Current.Session[SESSION_START] = DateTime.Now;
        }

        public DateTime Start
        {
            get
            {
                try
                {
                    return (DateTime)HttpContext.Current.Session[SESSION_START];
                }
                catch
                {
                    Initialize();
                }
                return (DateTime)HttpContext.Current.Session[SESSION_START];
            }
        }

        public DateTime Expired
        {
            get {
                double hours = SessionRules.SESSION_TIMEOUT_IN_HOURS * -1;
                return this.Start.AddHours(hours);
            }
        }

        public void AddProductToCart(int id, int qty)
        {
            if (HttpContext.Current.Session[SESSION_PRODUCTS] == null)
            {
                HttpContext.Current.Session.Add(SESSION_PRODUCTS, new Dictionary<int, int>());
            }
            Dictionary<int, int> myCart = (Dictionary<int, int>)HttpContext.Current.Session[SESSION_PRODUCTS];
            myCart.Add(id, qty);
            HttpContext.Current.Session[SESSION_PRODUCTS] = myCart;
        }
        
        public void RemoveProductFromCart(int id)
        {
            if (HttpContext.Current.Session[SESSION_PRODUCTS] != null)
            {
                Dictionary<int, int> myCart = (Dictionary<int, int>)HttpContext.Current.Session[SESSION_PRODUCTS];
                myCart.Remove(id);
                HttpContext.Current.Session[SESSION_PRODUCTS] = myCart;
            }
        }

        public Dictionary<int, int> GetCart()
        {
            return (Dictionary<int, int>)HttpContext.Current.Session[SESSION_PRODUCTS];
        }

        public void Clear()
        {
            if (SessionID != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }
    }
}