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
    public class ProdDetailController : ApiController
    {
        // GET: api/ProdDetail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProdDetail/5
        public ProductVM Get(int id)
        {
            ProductService service = new ProductService();
            return service.GetProductByID(id);
        }

        // POST: api/ProdDetail
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ProdDetail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProdDetail/5
        public void Delete(int id)
        {
        }
    }
}
