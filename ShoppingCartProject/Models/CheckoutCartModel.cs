using ShoppingCartProject.BusinessLogic.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartProject.Models
{
    public class CheckoutCartModel
    {
        public List<CartItemModel> CartItems { get; set; }
        private decimal SubTotal;
        private decimal Tax;
        private decimal Total;
        
        public decimal GetSubTotal()
        {
            SubTotal = 0;
            foreach (CartItemModel cartItem in CartItems)
            {
                SubTotal += cartItem.Price * cartItem.Quantity;
            }
            return Math.Round(SubTotal, CartRules.DECIMAL_DIGITS);
        }

        public decimal GetTax(decimal percentage = CartRules.TAX_PERCENTAGE)
        {
            Tax = Math.Round(GetSubTotal() * percentage, CartRules.DECIMAL_DIGITS);
            return Tax;
        }

        public decimal GetTotal()
        {
            Total = Math.Round(SubTotal + Tax, CartRules.DECIMAL_DIGITS);
            return Total;
        }
    }
}