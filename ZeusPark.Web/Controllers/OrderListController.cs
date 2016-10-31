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
    public class OrderListController : ApiController
    {

        public IEnumerable<OrderVM> Get()
        {
            OrderService service = new OrderService();
            var result = service.GetActiveOrders();
            return result;
        }

        public IEnumerable<OrderVM> Get(int userid)
        {
            //OrderService service = new OrderService();
            //var result = service.GetOrdersByUser(userid);
            //return result;
            return null;
        }

        // POST: api/ProdDetail
        public void Post([FromBody]OrderVM order)
        {

        }

        // PUT: api/ProdDetail/5
        public void Put(int id, [FromBody]int value)
        {
            OrderService service = new OrderService();
            service.UpdateOrderStatus(id, value);
        }

        // DELETE: api/ProdDetail/5
        public void Delete(int id)
        {
        }
    }
}
