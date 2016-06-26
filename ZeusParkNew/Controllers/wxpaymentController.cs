using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;
using WxPayAPI;
using ZeusParkNew.App_Start;

namespace ZeusParkNew.Controllers
{
    public class wxpaymentController : Controller
    {
        //
        // GET: /wxpayment/

        public ActionResult Index(FormCollection form)
        {
            string value = form["total"];
            double total = ViewBag.Total = Convert.ToDouble(value.Replace("¥", "")) * 100;
            ViewBag.Url = "/wxpayment/Image?total=" + total.ToString();


            return View();
        }

        public ActionResult Image(string total)
        {
            NativePay nativePay = new NativePay();
            
            string url2 = nativePay.GetPayUrl("123456789", total.ToString());

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
            image.Save(ms, ImageFormat.Png);

            //var buffer = found.Photo.ToArray();
            ImageConverter converter = new ImageConverter();
            var imageOut = (Image)converter.ConvertFrom(ms.GetBuffer());
            return new ImageResult()
            {
                Image = imageOut,
                ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            };

        }

    }
}
