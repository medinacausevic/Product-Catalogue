using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductCatalogue.Models;

namespace ProductCatalogue.Controllers
{
    public class ProductsController : Controller
    {
        private ProductEntities db = new ProductEntities();

        #region DB actions
        // GET: Products from DB
        public async Task<ActionResult> Index()
        {
            return View(await db.Product.OrderBy(p => p.Name).ToListAsync());
        }


        // Create actions
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Product.Add(product);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index", db.Product.OrderBy(p => p.Name).ToList());
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to input product. Try again!");
            }

            return View(product);
        }


        //Edit actions
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await db.Product.FindAsync(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", db.Product.OrderBy(p => p.Name).ToList());
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }

            return View(product);
        }
        #endregion


    }
}