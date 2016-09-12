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
    public class LoginParam
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }


    public class UserLoginController : ApiController
    {

        // POST api/<controller>
        public string Post([FromBody]LoginParam login)
        {
            string username = string.Empty;
            int accType = 0;
            UserService ser = new UserService();
            var result = ser.IsValidAccount(login.UserName, login.Password, out username, out accType);

            if (result > 0)
            {
                var cookiename = new HttpCookie("username", username);
                var cookieid = new HttpCookie("userid", result.ToString());
                var type = new HttpCookie("acctype", accType.ToString());
                HttpContext.Current.Response.Cookies.Add(cookieid);
                HttpContext.Current.Response.Cookies.Add(cookiename);
                HttpContext.Current.Response.Cookies.Add(type);

                return "OK";
            }

            return "Fail";
        }

        [HttpGet]
        public HttpResponseMessage loginwx(string code, string state)
        {
            WeChatProxy proxy = new WeChatProxy();
            string openid;
            int accType = 2;
            string accesstoken = proxy.GetAccessTokenByCode(code, out openid);

            WeChatUserInfo info = proxy.GetUserInfoByTokenOpenId(accesstoken, openid);

            UserService service = new UserService();
            int userid = 0;
            if (!service.IsWeChatAccountExist(info.OpenId, out userid, out accType))
            {
                userid = service.CreateWeChatAccount(info.Name, info.OpenId, info.HeadImgUrl, info.UnionId);
            }

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var cookiename = new HttpCookie("username", info.Name);
            var cookieid = new HttpCookie("userid", userid.ToString());
            var type = new HttpCookie("acctype", accType.ToString());
            HttpContext.Current.Response.Cookies.Add(cookieid);
            HttpContext.Current.Response.Cookies.Add(cookiename);
            HttpContext.Current.Response.Cookies.Add(type);

            response.Headers.Location = new Uri("http://www.zeuspark.com/");
            return response;

        }

    }




}