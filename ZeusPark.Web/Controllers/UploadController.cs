using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;

namespace ZeusPark.Web.Controllers
{

    public class UploadController : ApiController
    {
        private const string DetailTemplate = "<div class=\"detailcontent\"><div class=\"detailtext\">{0}</div>{1}</div>";
        private const string ImageTemplate = "<div class=\"item_pic_wrp\"><img class=\"item_pic\" src=\"{0}\" ></div>";
        //private const string ImagePath = "http://asset.zeuspark.com/newprodpic/";
        private const string ImagePath = "http://img.zeuspark.com/newprodpic/";
        private const string ImageTyple = ".jpg@!picstyle";

        public string Post(NewProduct newproduct)
        {
            try
            {
                ProductVM prod = new ProductVM();
                prod.ProductUnique = Guid.NewGuid().ToString();
                prod.Name = newproduct.prodName;
                prod.Status = 2;
                prod.BuyLimit = 1;

                AliService aliservice = new AliService();

                var mfiles = newproduct.mainfileids;
                if (null != mfiles && mfiles.Count > 0)
                {
                    aliservice.MoveImageFromTemp(mfiles[0]);
                    prod.MainImageUrl = ImagePath + mfiles[0] + ImageTyple;

                    if(mfiles.Count >= 2)
                    {
                        aliservice.MoveImageFromTemp(mfiles[1]);
                        prod.MainImageUrl2 = ImagePath + mfiles[1] + ImageTyple;
                    }

                    if (mfiles.Count == 3)
                    {
                        aliservice.MoveImageFromTemp(mfiles[2]);
                        prod.MainImageUrl3 = ImagePath + mfiles[2] + ImageTyple;
                    }

                }


                string otherFile = string.Empty;
                string detailContent = string.Empty;
                var ofiles = newproduct.otherfileids;
                if (null != ofiles && ofiles.Count > 0)
                {
                    for (int i = 0; i < ofiles.Count; i++)
                    {
                        string otherF = ImagePath + ofiles[i] + ImageTyple;
                        aliservice.MoveImageFromTemp(ofiles[i]);
                        string odiv = string.Format(ImageTemplate, otherF);
                        detailContent += odiv;
                    }
                }
                string detailhtml = string.Format(DetailTemplate, newproduct.detailDesc, detailContent);
                prod.DetailHtml = detailhtml;
                prod.Description = newproduct.simpleDesc;
                prod.Label = newproduct.keyword;
                prod.Groups.Add(new GroupVM() { GroupID = newproduct.groupid });
                prod.SkuInfoList = new List<SkuInfoVM>();
                foreach(var price in newproduct.priceDetail)
                {
                    var sku = new SkuInfoVM()
                    {
                        Quantity = price.quantity,
                        Price = price.price * 100,
                        OrgPrice = price.orgprice * 100,
                        Color = price.color,
                        Size = price.size
                    };

                    prod.SkuInfoList.Add(sku);
                }


                WechatDataImport import = new WechatDataImport();
                import.ImportProduct(prod);


                UserService service = new UserService();
                ProdUploadHistory history = new ProdUploadHistory();
                history.CategoryId = newproduct.keyword;
                history.Description = prod.DetailHtml;
                history.MainImg = prod.MainImageUrl;
                history.Name = prod.Name;
                history.Price = newproduct.price;
                history.ProdImg = otherFile;
                history.ProductUnique = prod.ProductUnique;
                history.UploadTime = DateTime.Now;
                history.UploadUser = newproduct.userid;

                service.AddUploadHistory(history);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            


            return "1";
        }

    }
}