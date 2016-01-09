using ShoppingCartProject.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models.Repositories
{
    public class CartItemRepo
    {
        ProductRepo prodRepo;

        public CartItemRepo(ProductRepo prodRepo)
        {
            this.prodRepo = prodRepo;
        }

        // TODO: Add Session in method parameters
        public CartItemModel GetCartItem(int id)
        {
            Product prod = prodRepo.GetProduct(id);

            CartItemModel item = new CartItemModel();
            item.ProductID = prod.productID;
            item.ProductName = prod.productName;
            item.Price = (decimal)prod.price;
            item.Quantity = CartRules.DEFAULT_QTY; // TODO: Check Session Qty

            return item;
        }

        // TODO: Add Session in method parameters
        public IEnumerable<CartItemModel> GetAllCartItems()
        {
            //TODO: Review/remove fake code below
            List<int> cartItems = new List<int> { 107, 329 };
            
            List<CartItemModel> cartItemsList = new List<CartItemModel>();

            foreach (int prodId in cartItems)
            {
                cartItemsList.Add(GetCartItem(prodId));
            }

            return cartItemsList;
            //TODO: -------------
        }
    }
}