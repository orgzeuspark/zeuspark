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
    public class MyOrderController : ApiController
    {

        public IEnumerable<OrderVM> Get(int id)
        {
            OrderService service = new OrderService();
            var result = service.GetOrdersByUser(id);
            return result;
        }

        public void Put(int id, [FromBody]int value)
        {
            OrderService service = new OrderService();
            service.UpdateOrderStatus(id, value);
        }


    }
}
