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

namespace ZeusPark.Web.Controllers
{

    public class WXPayController : ApiController
    {

        // POST api/<controller>
        public HttpResponseMessage Get(string id , string total)
        {
            NativePay nativePay = new NativePay();
            var wxtotal = Convert.ToDouble(total) * 100;
            string url2 = nativePay.GetPayUrl(id, wxtotal.ToString());

            //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeScale = 4;

            //将字符串生成二维码图片
            Bitmap image = qrCodeEncoder.Encode(url2, Encoding.Default);

            //保存为PNG到内存流  
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);

            //var buffer = found.Photo.ToArray();
            //ImageConverter converter = new ImageConverter();
            //var imageOut = (Image)converter.ConvertFrom(ms.GetBuffer());


            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);
            Response.Content = new ByteArrayContent(ms.GetBuffer());
            Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return Response;
            //return new ImageResult()
            //{
            //    Image = imageOut,
            //    ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            //};
        }

    }
}