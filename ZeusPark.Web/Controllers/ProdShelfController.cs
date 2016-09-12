using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZeusPark.Service.Model;
using ZeusPark.Service.Product;

namespace ZeusPark.Web.Controllers
{

    public class ShelfStatus
    {
        public int productId { get; set; }

        public bool isOnShelf { get; set; }
    }
    public class ProdShelfController : ApiController
    {
        // GET: api/ProdList
        public IEnumerable<ProductVM> Get()
        {
            ProductService service = new ProductService();
            var list = service.GetAllProductsWithMainFields();
            return list;
        }

        // POST: api/ProdList
        [HttpPost]
        public void Post([FromBody]ShelfStatus shelf)
        {
            ProductService service = new ProductService();
            if (shelf.isOnShelf)
            {
                service.UpdateProductStatus(shelf.productId, 1);
            }
            else
            {
                service.UpdateProductStatus(shelf.productId, 2);
            }
        }


        // PUT: api/ProdList/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProdList/5
        public void Delete(int id)
        {
            ProductService service = new ProductService();
            service.DeleteProductByID(id);
        }
    }
}
