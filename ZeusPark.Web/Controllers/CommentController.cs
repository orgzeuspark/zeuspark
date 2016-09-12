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
    public class CommentController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IEnumerable<CommentVM> Get(int id)
        {
            CommentService service = new CommentService();
            var result = service.GetCommentByProductID(id);
            return result;
        }


    }
}
