using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikesShop.Models;

namespace BikesShop.Controllers
{
    public class ProductsController : Controller
    {
        private BikeOnineShopDBContext db = new BikeOnineShopDBContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

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
                    ViewBag.RateAverage = rateAvg.ToString();
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


        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Category,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Category,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Search(string SearchText)
        {

            if (!String.IsNullOrEmpty(SearchText))
            {
                string temp = SearchText.ToLower();

                var products = from m in db.Products
                               select m;
                products = products.Where(s => s.Name.ToLower().Contains(temp) && s.Category.ToLower().Contains(temp));

                return View(products.ToList());
            }
            else
            {
                ViewBag.SearchSuccess = 0;
                return RedirectToAction("Index","Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
