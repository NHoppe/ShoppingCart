using ShoppingCartProject.BusinessLogic.Constants;
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

        public CartItemModel GetCartItem(int id, int qty = CartRules.DEFAULT_QTY)
        {
            Product prod = prodRepo.GetProduct(id);

            CartItemModel item = new CartItemModel();
            item.ProductID = prod.productID;
            item.ProductName = prod.productName;
            item.Price = (decimal)prod.price;
            item.Quantity = qty;

            return item;
        }

        public IEnumerable<CartItemModel> GetAllCartItems(Dictionary<int, int> sessionCart)
        {
            List<CartItemModel> cartItemsList = new List<CartItemModel>();

            foreach (KeyValuePair<int, int> sessionCartItem in sessionCart)
            {
                // Key = ProdID; Value = Qty
                CartItemModel cartItem = GetCartItem(sessionCartItem.Key, sessionCartItem.Value);
                cartItemsList.Add(cartItem);
            }
            
            return cartItemsList;
        }
    }
}