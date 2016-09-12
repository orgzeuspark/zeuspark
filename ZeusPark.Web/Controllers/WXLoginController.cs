using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;

namespace ZeusPark.Web.Controllers
{
    public class WXLoginController : ApiController
    {
        // GET: api/WXLogin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WXLogin/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WXLogin
        public void Post(string code, string state)
        {
            WeChatProxy proxy = new WeChatProxy();
            string openid;
            int accType;
            string accesstoken = proxy.GetAccessTokenByCode(code, out openid);

            WeChatUserInfo info = proxy.GetUserInfoByTokenOpenId(accesstoken, openid);

            UserService service = new UserService();
            int userid = 0;
            if (!service.IsWeChatAccountExist(info.OpenId, out userid, out accType))
            {
                userid = service.CreateWeChatAccount(info.Name, info.OpenId, info.HeadImgUrl, info.UnionId);
            }

            
        }

        // PUT: api/WXLogin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WXLogin/5
        public void Delete(int id)
        {
        }
    }
}
