using BikesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikesShop.ViewModels;

namespace BikesShop.Controllers
{

   
    public class ShoppingCartController : Controller
    {
        BikeOnineShopDBContext storeDB = new BikeOnineShopDBContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedProduct = storeDB.Products
                .Single(P => P.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string proName = storeDB.Carts
                .Single(item => item.RecordId == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(proName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }

        public ActionResult Checkout()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            var customer = (from cust in storeDB.Customers
                            where cust.Email == User.Identity.Name 
                            select new { cust.ID , cust.Name }).FirstOrDefault();

            int orderId = 1;

            var order = (from ord in storeDB.Orders
                         select new { ord.OrderNumber , ord.OrderDate }).OrderByDescending(a=>a.OrderDate).FirstOrDefault();

            if (order != null)
            {
                orderId = Int32.Parse(order.OrderNumber) + 1;
            }

            Order or = new Order();
            or.OrderNumber = orderId.ToString();
            or.Customer_Id = customer.ID;
            or.OrderDate = DateTime.Now;
            or.total = decimal.Parse(viewModel.CartTotal.ToString());
            or.Username = customer.Name;
            storeDB.Orders.Add(or);
            storeDB.SaveChanges();
            
            foreach (var item in viewModel.CartItems)
            {

                OrdersDetail od = new OrdersDetail();
                od.OrderNumber = orderId.ToString();
                od.ProductId = item.ProductId;
                od.Unitprice = decimal.Parse(item.Product.Price.ToString());
                od.Quantity = item.Count;
             
                storeDB.OrdersDetails.Add(od);
                storeDB.SaveChanges();

                for (int i = 0; i < item.Count; i++)
                {
                    RemoveFromCart(item.RecordId);
                }
            }
            return RedirectToAction("Index");
        }

    }
}