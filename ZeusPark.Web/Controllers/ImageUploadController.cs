using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using ZeusPark.Service.Admin;

namespace ZeusPark.Web.Controllers
{

    public class ImageUploadController : ApiController
    {
        

        // POST api/<controller>
        //public string Post(FormDataCollection formData, IEnumerable<HttpPostedFileBase> mainfiles, IEnumerable<HttpPostedFileBase> otherfiles)
        public string Post()
        {
            if ("/api/ImageUpload/remove".Equals(HttpContext.Current.Request.FilePath))
            {
                return "2";
            }

            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            var picid = HttpContext.Current.Request.Form["picid"] as string;
            if (file != null && file.ContentLength > 0)
            {

                //ImageWater_Word word = new ImageWater_Word();
                //word.DrawWords(, "ZEUSPARK", 0.5f, ImagePosition.RigthBottom, true);

                var fileName = Path.GetFileName(file.FileName);
                byte[] fileData = null;
                //file.InputStream.Position = 0;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileData = binaryReader.ReadBytes(file.ContentLength);
                }

                AliService service = new AliService();
                service.UploadTempImage(fileData, picid);

            }

            return "1";
        }

    }
}