using BikesShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BikesShop.Controllers
{

    public class HomeController : Controller
    {
        private BikeOnineShopDBContext db = new BikeOnineShopDBContext();
        public ActionResult Index()
        {
            var products = GetTopSellingProducts(6);

            return View(products);
        }

        public ActionResult AllProducts()
        {
            int count = db.Products.Count();
            var products= db.Products
              .OrderByDescending(a => a.OrdersDetails.Count())
              .Take(count)
              .ToList();
            return View(products);
        }
        private List<Product> GetTopSellingProducts(int count)
        {
            // Group the order details by product and return
            // the product with the highest count

            return db.Products.OrderByDescending(a => a.OrdersDetails.Count()).Take(count).ToList();
             
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        //to configure action that admin choose(edit,details,delete)

        public ActionResult ChhoseActionProduct(int? Id, string Command1)
        {

            return RedirectToAction(Command1 + "Product", "Home", new { Id = Id });

        }

        [HttpGet]
        public ActionResult EditProduct(int? Id)
        {
            var p = db.Products.Find(Id);
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product pro, string Command1)
        {
            if (Command1.Equals("Save") && Command1 != null)
            {
                db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex", "Home");
            }
            else
            {
                return RedirectToAction("AdminIndex", "Home");
            }


        }

        public ActionResult DetailsProduct(int? id)
        {
            var pro = db.Products.Find(id);

            return View(pro);
        }

        [HttpGet]
        public ActionResult DeleteProduct(int? id)
        {
            var p = db.Products.Find(id);
            return View(p);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int? Id, string Command)
        {
            if (Command.Equals("Delete"))
            {
                Product pro = db.Products.Find(Id);
                db.Products.Remove(pro);
                db.SaveChanges();
                ViewData["RemovesSuccess"] = "Product (" + pro.Name + ") removed successfully";
                return RedirectToAction("AdminIndex", "Home");
            }
            else
            {
                return RedirectToAction("AdminIndex", "Home");
            }


        }

        public ActionResult CrerateProduct(string command)
        {

            return View();
        }

        //for admin to create new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product pro, string Command1)
        {
            if (Command1.Equals("Create") && Command1 != null)
            {
               

                if(pro.Price.Equals(null) || pro.Name.Equals(null) || pro.Category.Equals(null) ||pro.ImageName.Equals(null))
                {
                    ViewBag.Failed = "Please fill the fileds";
                    return View("CrerateProduct");

                }

                string proName = pro.Name;
                var checkExist = (from p in db.Products where p.Name == proName select p).ToList();
                if(checkExist.FirstOrDefault()!=null || checkExist.Count!=0)
                {
                    ViewBag.Exist = "Product (" + pro.Name + ") Already Exist..";
                    return View("CrerateProduct");
                }
                
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("AdminIndex", "Home");
            }
            else
            {
                return RedirectToAction("AdminIndex", "Home");
            }
        }

        public ActionResult ShowProducts()
        {

            return View(db.Products.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        //public ActionResult Statistics()
        //{
        //    return View();
        //}

        //public ActionResult AdminReports(DateReviewer d, string Command1)
        //{
        //    if (Command1.Equals("Produce Report"))

        //        return RedirectToAction("ProduceReport", "Home",d);
        //    else if (Command1.Equals("Draw Chart"))
        //        return RedirectToAction("DrawChart", "Home");
        //    else
        //        return RedirectToAction("AdminIndex", "Home");
        //}

        public ActionResult ProduceReport(DateReviewer d)
        {

            var orderDates = (from orders in db.Orders.Where(o => o.OrderDate >= d.StartDate &&
                          o.OrderDate <= d.EndDate)
                              group orders by orders.OrderDate into ord


                              select new
                              {
                                  OrderDate = ord.FirstOrDefault().OrderDate,
                                  total = ord.FirstOrDefault().total

                              }
                        ).ToList();

            ArrayList dates = new ArrayList();
            ArrayList Totals = new ArrayList();
            DateTime end = d.StartDate.Value.AddDays(orderDates.Count - 1);
            while (d.StartDate <= end)
            {


                Totals.Add((from or in db.Orders
                            where or.OrderDate.Value.Day == d.StartDate.Value.Day
                            select or.total).Sum());

                d.StartDate = d.StartDate.Value.AddDays(1);
            }
            orderDates.ToList().ForEach(od => dates.Add(od.OrderDate));

            new System.Web.Helpers.Chart(width: 1000, height: 400).AddSeries
                (chartType: "column", xValue: dates, yValues: Totals).Write("jpeg");

            return null;
        }

        [HttpPost]

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
        public ActionResult Cancel()
        {
            return RedirectToAction("AdminIndex", "Home");
        }

    }
}