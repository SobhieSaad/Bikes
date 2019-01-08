using BikesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikesShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}