using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;

namespace ZeusPark.Web.Controllers
{

    public class UserRegisterController : ApiController
    {

        // POST api/<controller>
        public string Put([FromBody]UserAccount useraccount)
        {
            UserService service = new UserService();
            service.CreateAccount(useraccount);

            return "OK";
        }

        public bool Post([FromBody]UserAccount useraccount)
        {
            UserService service = new UserService();
            var result = service.IsAccountExist(useraccount);

            return result;
        }

    }




}