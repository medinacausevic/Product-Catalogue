using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductCatalogue.Models;

namespace ProductCatalogue.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductEntities db = new ProductEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult GetData()
        {
            using (ProductEntities db = new ProductEntities())
            {
                List<Product> productList = db.Products.ToList<Product>();
                return Json(new { data = productList }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}