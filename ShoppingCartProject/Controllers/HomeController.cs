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

        [HttpGet]
        public ActionResult Update(int prodId, int qty)
        {
            if (qty < 1)
            {
                return RedirectToAction("WrongQty", new { prodId = prodId, qty = qty });
            }
            // TODO: Add Item to Session;
            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public ActionResult ViewCart()
        {
            // TODO: Read IDs and Qtys from Session
            // TODO: Remove fake code below
            A00964856_ShoppingCartEntities db = new A00964856_ShoppingCartEntities();
            ProductRepo prodRepo = new ProductRepo(db);
            CartItemRepo cartItemRepo = new CartItemRepo(prodRepo);
            IEnumerable<CartItemModel> cartItems = cartItemRepo.GetAllCartItems();
            // TODO: -------

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