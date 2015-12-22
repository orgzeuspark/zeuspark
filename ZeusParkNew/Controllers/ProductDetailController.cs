using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeusPark.Service.Product;

namespace ZeusPark.Web.Controllers
{
    public class ProductDetailController : Controller
    {
        //
        // GET: /ProductDetail/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ProductDetail/Details/5

        public ActionResult Details(int id)
        {
            ProductService service = new ProductService();
            var prod = service.GetProductByID(id);

            return View(prod);
        }

        public ActionResult ProdList(int id)
        {
            ProductService service = new ProductService();
            var list = service.GetProductGroupList(id);

            return View(list);
        }

        //
        // GET: /ProductDetail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ProductDetail/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ProductDetail/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ProductDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ProductDetail/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ProductDetail/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
