using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using ThoughtWorks.QRCode.Codec;
using WxPayAPI;
using ZeusPark.Service.Admin;
using ZeusParkNew;

namespace ZeusPark.Web.Controllers
{

    public class AliPayController : ApiController
    {

        public HttpResponseMessage Get(string subject, string totalFee)
        {
            var result = AlipayParamHelper.BuildParamters(subject, totalFee);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(result);// new ByteArrayContent(ms.GetBuffer());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        

    }
}