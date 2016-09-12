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
    public class QuestionController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IEnumerable<QuestionVM> Get(int id)
        {
            CommentService service = new CommentService();
            var result = service.GetQuestionsByProductID(id);
            return result;
        }

        // POST: api/ProdDetail
        public void Post([FromBody]QuestionVM question)
        {
            CommentService service = new CommentService();
            service.AddQuestion(question);


        }

    }
}
