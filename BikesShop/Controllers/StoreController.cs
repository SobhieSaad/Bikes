using BikesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikesShop.Controllers
{
    public class StoreController : Controller
    {
        private BikeOnineShopDBContext db = new BikeOnineShopDBContext();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddRate(Product pr, int Id)
        //{
        //    int rate = pr.ProductCommentRate.ProductRate.Rate;
        //    //invaled rate
        //    if (rate <= 0 || rate > 5 || rate.Equals(null))
        //        return RedirectToAction("Details", "Store", new { id = Id });

        //    int proId = Id;
        //    string customerEmail = User.Identity.Name;

        //    var customerId = (from c in db.Customers
        //                      where c.Email == customerEmail
        //                      select c).ToList();
        //    //add new rate to Product Rate table
        //    ProductRate ProRate = new ProductRate();
        //    int cId = customerId.FirstOrDefault().ID;
        //    var OldExistCustRate = (from d in db.ProductRates where d.CustomerId == cId select d).ToList();
        //    if (OldExistCustRate.FirstOrDefault() != null)
        //    {
        //        ProRate = db.ProductRates.Find(OldExistCustRate.FirstOrDefault().Id);
        //        db.ProductRates.Remove(ProRate);
        //        db.SaveChanges();
        //    }
        //    ProRate.CustomerId = customerId.FirstOrDefault().ID;
        //    ProRate.ProductId = proId;
        //    ProRate.RateDate = System.DateTime.Now;
        //    ProRate.Product = db.Products.Find(proId);
        //    ProRate.Customer = db.Customers.Find(customerId.FirstOrDefault().ID);
        //    ProRate.Rate = rate;
        //    db.ProductRates.Add(ProRate);
        //    db.SaveChanges();
        //    return RedirectToAction("Details", "Store", new { id = Id });
        //}

        //[HttpPost]
        //public ActionResult AddComment(Product pcr, string Command1, int Id)
        //{

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        string comment = pcr.ProductCommentRate.CustomersComment.Comment.ToString();

        //        int proId = Id;

        //        string customerEmail = User.Identity.Name;

        //        var customerId = (from c in db.Customers
        //                          where c.Email == customerEmail
        //                          select c).ToList();
        //        CustomersComment cc = new CustomersComment();
        //        cc.Comment = comment;
        //        cc.CustomerId = customerId.FirstOrDefault().ID;
        //        cc.ProductId = proId;
        //        cc.CommentDate = System.DateTime.Now;
        //        cc.Customer = db.Customers.Find(customerId.FirstOrDefault().ID);
        //        cc.Product = db.Products.Find(proId);
        //        db.CustomersComments.Add(cc);
        //        db.SaveChanges();

        //        return RedirectToAction("Details", "Store", new { id = Id });
        //    }
        //    else
        //    {
        //        return RedirectToAction("Details", "Store", new { id = Id });
        //    }
        //}
        public ActionResult Details(int id)
        {
            var pro = db.Products.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var rateSum = (from rte in db.ProductRates
                               where rte.ProductId == pro.Id
                               select rte).ToList();
                if (rateSum.Count != 0)
                {
                    int total = 0;
                    foreach (var v in rateSum)
                    {
                        total += v.Rate;
                    }
                    var rateAvg = total / rateSum.Count;
                    if (pro.AverageRate.Equals(null) || pro.AverageRate == 0 || pro.AverageRate != rateAvg)
                    {
                        pro.AverageRate = rateAvg;
                    }

                    //get user id and average to show old customer rate
                    ///////////////////////////////////////////
                    string customerEmail = User.Identity.Name;

                    var customerId = (from c in db.Customers
                                      where c.Email == customerEmail
                                      select c).ToList();
                    int Oldrate;
                    int cId = customerId.FirstOrDefault().ID;
                    var OldExistCustRate = (from d in db.ProductRates where d.CustomerId == cId select d).ToList();
                    if (OldExistCustRate.FirstOrDefault() != null)
                    {
                        Oldrate = customerId.FirstOrDefault().ProductRates.FirstOrDefault().Rate;
                    }
                    else
                        Oldrate = 0;
                    if (Oldrate.Equals(null) || Oldrate == 0)
                        ViewData["OldRate"] = 0;
                    else
                        ViewData["OldRate"] = Oldrate;

                    /////////////////////////////////////////

                    ViewData["Average"] = rateAvg;
                    if (rateAvg > 0)
                        ViewBag.RateAverage = rateAvg.ToString();
                    else
                       ViewBag.RateAverage = 0;
                }

                else
                {
                    ViewData["Average"] = 0;
                    ViewData["OldRate"] = 0;

                    ViewBag.RateAverage = 0;

                }
            }
            else
            {
                var rateSum = (from rte in db.ProductRates
                               where rte.ProductId == pro.Id
                               select rte).ToList();
                if (rateSum.Count != 0)
                {
                    int total = 0;
                    foreach (var v in rateSum)
                    {
                        total += v.Rate;
                    }
                    var rateAvg = total / rateSum.Count;
                    if (pro.AverageRate.Equals(null) || pro.AverageRate == 0 || pro.AverageRate != rateAvg)
                    {
                        pro.AverageRate = rateAvg;
                    }
                    ViewData["OldRate"] = 0;
                    ViewData["Average"] = rateAvg;
                }
                else
                {
                    ViewData["Average"] = 0;
                    ViewData["OldRate"] = 0;

                    ViewBag.RateAverage = 0;
                }
            }
            return View(pro);
        }


        public ActionResult Browse(string genre)
        {

            var genreModel = db.Products.Include("Products")
                .Single(g => g.Name == genre);

            return View(genreModel);
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var categories = (from cat in db.Products select new { cat.Category }).ToList();


            return PartialView(categories);
        }
    }
}