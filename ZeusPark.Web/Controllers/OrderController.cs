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
    public class OrderController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public OrderVM Get(int id)
        {
            OrderService service = new OrderService();
            var result = service.GetOrder(id);
            return result;
            
        }

        // POST: api/ProdDetail
        public void Post([FromBody]OrderVM order)
        {
            try
            {
                //string orderunique = Guid.NewGuid().ToString().Replace("-", "");
                //order.OrderUnique = orderunique;
                OrderService service = new OrderService();
                service.CreateOrder(order);
            }
            catch (Exception ex)
            {
                throw;
            }


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
