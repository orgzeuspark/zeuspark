using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WxPayAPI;

namespace ZeusParkNew.Controllers
{
    public class wxpaytestController : Controller
    {
        //
        // GET: /wxpaytest/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult product()
        {
            JsApiPay jsApiPay = new JsApiPay();
            try
            {
                //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                string redirect_uri = HttpUtility.UrlEncode("http://www.zeuspark.com/wxpaytest/");
                WxPayData data = new WxPayData();
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("redirect_uri", redirect_uri);
                data.SetValue("response_type", "code");
                data.SetValue("scope", "snsapi_base");
                data.SetValue("state", "STATE" + "#wechat_redirect");
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();

                Redirect(url);

                //获取收货地址js函数入口参数
                //ViewBag.wxEditAddrParam = jsApiPay.GetEditAddressParameters();
                //ViewBag.OpenId = jsApiPay.openid;

            }
            catch (Exception ex)
            {
            }

            return View();
        }

        

    }
}
