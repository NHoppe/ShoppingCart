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
            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();
            ProductRepo prodRepo = new ProductRepo(db);
            CartItemRepo cartItemRepo = new CartItemRepo(prodRepo);
            CartItemModel item = cartItemRepo.GetCartItem(prodId);

            return View(item);
        }

        [HttpPost]
        public ActionResult Update(CartItemModel cartItem)
        {
            if (cartItem.Quantity < 1)
            {
                return RedirectToAction("WrongQty", new { prodId = cartItem.ProductID, qty = cartItem.Quantity});
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
            
            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public ActionResult ViewCart()
        {
            SessionHelper sessionHlp = new SessionHelper();
            Dictionary<int, int> sessionCart = sessionHlp.GetCart();
            
            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();
            ProductRepo prodRepo = new ProductRepo(db);
            CartItemRepo cartItemRepo = new CartItemRepo(prodRepo);
            IEnumerable<CartItemModel> cartItems = cartItemRepo.GetAllCartItems(sessionCart);

            return View(cartItems);
        }

        [HttpGet]
        public ActionResult WrongQty(int? prodId, int? qty)
        {
            ViewBag.ProdId = (prodId != null)? prodId : 0;
            ViewBag.Qty = (qty != null)? qty : 0;
            return View();
        }
    }
}