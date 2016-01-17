using ShoppingCartProject.BusinessLogic;
using ShoppingCartProject.Models;
using ShoppingCartProject.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();
            ProductRepo prodRepo = new ProductRepo(db);
            
            return View(prodRepo.GetProducts());
        }

        [HttpGet]
        public ActionResult Add(int prodId)
        {
            SessionHelper sessonHlp = new SessionHelper();
            int qty = sessonHlp.GetProductQtyFromCart(prodId);

            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();
            ProductRepo prodRepo = new ProductRepo(db);
            CartItemRepo cartItemRepo = new CartItemRepo(prodRepo);
            CartItemModel item = cartItemRepo.GetCartItem(prodId, qty);

            return View(item);
        }

        [HttpGet]
        public ActionResult Remove(int prodId)
        {
            SessionHelper sessionHlp = new SessionHelper();
            sessionHlp.RemoveProductFromCart(prodId);
            return RedirectToAction("ViewCart");
        }
        
        private bool UpdateCart(CartItemModel cartItem)
        {
            if (cartItem.Quantity < 1) {
                return false;
            }

            SessionHelper sessionHlp = new SessionHelper();
            sessionHlp.AddProductToCart(cartItem.ProductID, cartItem.Quantity);

            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();

            ProductRepo prodRepo = new ProductRepo(db);
            VisitRepo visitRepo = new VisitRepo(db);
            ProductVisitRepo prodVisitRepo = new ProductVisitRepo(db);

            Visit visit = visitRepo.GetVisit(sessionHlp.SessionID);
            Product product = prodRepo.GetProduct(cartItem.ProductID);
            prodVisitRepo.AddProductVisit(visit, product, cartItem.Quantity);

            return true;
        }

        [HttpPost]
        public ActionResult Update(CartItemModel cartItem)
        {
            if (!UpdateCart(cartItem))
            {
                return RedirectToAction("WrongQty", new { prodId = cartItem.ProductID, qty = cartItem.Quantity });
            }
            
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public ActionResult SaveOrder(CheckoutCartModel checkoutCart)
        {
            if (ModelState.IsValid)
            {
                foreach (CartItemModel cartItem in checkoutCart.CartItems)
                {
                    if (!UpdateCart(cartItem))
                    {
                        return RedirectToAction("WrongQty", new { prodId = cartItem.ProductID, qty = cartItem.Quantity });
                    }
                }
            }

            return RedirectToAction("ThankYou");
        }

        [HttpGet]
        public ActionResult ViewCart()
        {
            SessionHelper sessionHlp = new SessionHelper();
            Dictionary<int, int> sessionCart = sessionHlp.GetCart();
            
            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();
            ProductRepo prodRepo = new ProductRepo(db);
            CartItemRepo cartItemRepo = new CartItemRepo(prodRepo);
            List<CartItemModel> cartItems = cartItemRepo.GetAllCartItems(sessionCart);

            CheckoutCartModel checkoutCart = new CheckoutCartModel();
            checkoutCart.CartItems = cartItems;

            return View(checkoutCart);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return RedirectToAction("ThankYou");
        }

        [HttpGet]
        public ActionResult ThankYou()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CancelOrder()
        {
            SessionHelper sessionHlp = new SessionHelper();

            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();

            VisitRepo visitRepo = new VisitRepo(db);
            visitRepo.ClearVisit(sessionHlp.SessionID);

            sessionHlp.Clear();

            return RedirectToAction("ThankYou");
        }

        //*************************************************
        // ERROR DISPLAY
        //*************************************************
        [HttpGet]
        public ActionResult WrongQty(int? prodId, int? qty)
        {
            ViewBag.ProdId = (prodId != null)? prodId : 0;
            ViewBag.Qty = (qty != null)? qty : 0;
            return View();
        }
    }
}