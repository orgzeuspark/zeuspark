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
    public class ProdListController : ApiController
    {
        // GET: api/ProdList
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProdList/5
        public ProductGroupVM Get(int id)
        {
            ProductService service = new ProductService();
            var list = service.GetProductGroupList(id);

            return list;
        }

        // POST: api/ProdList
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ProdList/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProdList/5
        public void Delete(int id)
        {
        }
    }
}
