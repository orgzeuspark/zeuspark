using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;
using System.IO;
using System.Collections.Specialized;

namespace ZeusPark.Web.Models
{

    public class WeChatProxy
    {
        public const string AccessTokenKey = "AccessToken";
        public const string AccessTokenTime = "AccessTokenTime";
        private static string Appid = ConfigurationManager.AppSettings["WechatAppid"];
        private static string Secret = ConfigurationManager.AppSettings["WechatSecret"];
        private static string AccessTokenUrl = ConfigurationManager.AppSettings["WechatAccessTokenURL"];
        private static string GroupUrl = ConfigurationManager.AppSettings["WechatGroupURL"];
        private static string GroupByIdUrl = ConfigurationManager.AppSettings["WechatByGroupidUrl"];
        private static string ProductUrl = ConfigurationManager.AppSettings["WechatProductUrl"];
        private static string ImageUploadUrl = ConfigurationManager.AppSettings["WechatUploadImageUrl"];
        private static string ProductUploadUrl = ConfigurationManager.AppSettings["WechatUploadProductUrl"];


        public string GetAccessTokenByCode(string code, out string openid)
        {
            string url = string.Format(@"https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx7784b75803f93ee2&secret=5ee2a32b23bea686f7b802bb20e8e82a&code={0}&grant_type=authorization_code", code);

            dynamic result = GetHttpData(url);
            openid = result["openid"];
            return result["access_token"];
        }

        public WeChatUserInfo GetUserInfoByTokenOpenId(string accessToken, string openId)
        {
            string url = string.Format(@"https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", accessToken, openId);
            dynamic result = GetHttpData(url);

            WeChatUserInfo info = new WeChatUserInfo();
            info.Name = result["nickname"];
            info.OpenId = result["openid"];
            info.Sex = Convert.ToInt32(result["sex"]);
            info.HeadImgUrl = result["headimgurl"];
            info.UnionId = result["unionid"];

            return info;
        }

        public string AccessToken
        {
            get
            {
                string token = string.Empty;
                if (HttpContext.Current.Cache[AccessTokenKey] == null)
                {
                    token = GetAccessToken();
                    HttpContext.Current.Cache[AccessTokenKey] = token;
                    HttpContext.Current.Cache[AccessTokenTime] = DateTime.UtcNow;
                }
                else if (HttpContext.Current.Cache[AccessTokenTime] != null &&
                    DateTime.UtcNow.Subtract(((DateTime)HttpContext.Current.Cache[AccessTokenTime])).Seconds > 7000)
                {
                    token = GetAccessToken();
                    HttpContext.Current.Cache[AccessTokenKey] = token;
                    HttpContext.Current.Cache[AccessTokenTime] = DateTime.UtcNow;
                }
                else
                {
                    token = HttpContext.Current.Cache[AccessTokenKey] as string;
                }

                return token;
            }
        }

        public Collection<GroupVM> GetGroups()
        {
            string url = string.Format(GroupUrl, AccessToken);
            dynamic groups = GetHttpData(url);
            Collection<GroupVM> groupsVM = new Collection<GroupVM>();
            foreach (var group in groups["groups_detail"])
            {
                GroupVM groupVM = new GroupVM();
                groupVM.GroupID = group["group_id"];
                groupVM.GroupName = group["group_name"];
                groupsVM.Add(groupVM);
            }
            return groupsVM;
        }

        public List<string> GetProductListByGroup(int groupId)
        {
            string url = string.Format(GroupByIdUrl, AccessToken);
            string postData = "{\"group_id\": " + groupId.ToString() +"}";
            dynamic result = PostHttpData(url, postData);
            List<string> productIdList = new List<string>();

            foreach (var pro in result["group_detail"]["product_list"])
            {
                productIdList.Add(pro);
            }

            return productIdList;
        }

        public void GetMaterailList(int begin, ref string mediaId, ref string urlvalue)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", AccessToken);
            string postData = "{\"type\": \"image\",\"offset\":" + begin + ",\"count\":" + 1 + "}";
            dynamic result = PostHttpData(url, postData);
            var totalcount = result["total_count"];
            var itemcount = result["item_count"];
            var items = result["item"];
            foreach (var item in items)
            {
                if(item.Count == 4)
                {
                    mediaId = item["media_id"];
                    var name = item["name"];
                    urlvalue = item["url"];
                }
                else
                {
                    mediaId = string.Empty;
                    urlvalue = string.Empty;
                    return;
                }

            }
        }

        public byte[] GetMaterial(string id)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}", AccessToken);
            string postData = "{\"media_id\":\"" + id + "\"}";

            Uri uri = new Uri(url, UriKind.Absolute);

            using (WebClient client = new WebClient())
            {
                //client.Encoding = System.Text.Encoding.UTF8;
                //NameValueCollection form = new NameValueCollection();
                //form.Add("media_id", id);
                Byte[] request = System.Text.Encoding.Default.GetBytes(postData);
                Byte[] responseData = client.UploadData(url, request);
                //var data = client.UploadString(uri, postData);
                return responseData;
                //return System.Text.Encoding.Default.GetBytes(data);
            }

        }

        public ProductVM GetProductByID(string productId)
        {

            string url = string.Format(ProductUrl, AccessToken);
            string postData = "{\"product_id\": \"" + productId + "\"}";
            dynamic result = PostHttpData(url, postData);

            try
            {
                var prodInfo = result["product_info"];
                var prodBase = prodInfo["product_base"];
                ProductVM productVM = new ProductVM();
                productVM.ProductUnique = prodInfo["product_id"];
                productVM.Name = prodBase["name"];
                productVM.CategoryID = prodBase["category_id"][0];
                foreach (var property in prodBase["property"])
                {
                    productVM.Properties.Add(property["id"], property["vid"]);
                }
                foreach (var img in prodBase["img"])
                {
                    productVM.Images.Add(new ImageVM() { ImageUrl = img });
                }
                productVM.DeliveryInfo = new DeliveryInfoVM()
                {
                    DeliveryType = prodInfo["delivery_info"]["delivery_type"],
                    TemplateId = Convert.ToString(prodInfo["delivery_info"]["template_id"])
                };

                foreach (var sku in prodInfo["sku_list"])
                {
                    productVM.SkuInfoList.Add(new SkuInfoVM()
                    {
                        skuId = sku["sku_id"],
                        OrgPrice = sku["ori_price"],
                        Price = sku["price"],
                        IconUrl = sku["icon_url"],
                        Quantity = sku["quantity"],
                        ProductCode = sku["product_code"]
                    });
                }

                productVM.DetailHtml = prodBase["detail_html"];
                productVM.DetailHtml = productVM.DetailHtml.Replace("http://mmbiz.qpic.cn/mmbiz/", "http://asset.zeuspark.com/prodpic/");
                productVM.DetailHtml = productVM.DetailHtml.Replace("/0\"", "/0.jpg\"");
                productVM.DetailHtml = productVM.DetailHtml.Replace("/0?wx_fmt=jpeg\"", "/0.jpg\"");
                productVM.DetailHtml = productVM.DetailHtml.Replace("/0?wx_fmt=png\"", "/0.jpg\"");
                productVM.MainImageUrl = prodBase["main_img"];
                productVM.MainImageUrl = productVM.MainImageUrl.Replace(@"http://mmbiz.qpic.cn/mmbiz/", "http://asset.zeuspark.com/prodpic/") + ".jpg";
                productVM.MainImageUrl = productVM.MainImageUrl.Replace("?wx_fmt=jpeg", "");
                productVM.MainImageUrl = productVM.MainImageUrl.Replace("?wx_fmt=png", "");

                productVM.BuyLimit = prodBase["buy_limit"];
                productVM.Status = prodInfo["status"];

                return productVM;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllLines("error.log", new List<string>() {"id: " + productId + ";" + ex.Message});
                return null;
            }
            
        }

        public void GetPayImage(string productUnique)
        {
            //string url = @"https://mp.weixin.qq.com/merchant/goods?type=12&pid=pyKYTt8Eljc67CMj9b4dvrr4zJYo&token=1376367825&lang=zh_CN&pixsize=224";
            string url = @"https://mp.weixin.qq.com/merchant/goods?type=12&pid=" + productUnique + "&token=1376367825&lang=zh_CN&pixsize=224";
            //string jsonResponse = string.Empty;
            //Uri uri = new Uri(url, UriKind.Absolute);
            using (WebClient client = new WebClient())
            {
                //client.Headers.Add(HttpRequestHeader.Cookie, "data_bizuin=3096478617; data_ticket=AgUIJyufHKdCQnnLUA9My5DuAwE3CBi5cckBErf47nk=; slave_user=gh_a98b7e0d1f3e; slave_sid=Zm1rOWJsVkdJMGE1MnZOSkZyQ3g2QjJmNFdRT1BKVnptQTNvbzFfN0JkOXkyNHYwMW1LajhBN2Y3Q2xHWkpSem4wU2kwN2xWT0piSlNOeW5ZUnJZM1dFUlNLNUFGQzRHQUxlMTJBdmgyQ1piVUw3cVdWQ2hGYnJ0NU9WWUs0czA=; bizuin=3071518408; remember_acct=zhangjiaming122%40hotmail.com");
                client.Headers.Add(HttpRequestHeader.Cookie, "remember_acct=zhangjiaming122%40hotmail.com; data_bizuin=3096478617; data_ticket=AgVL2qZlLqae/VTgXtp7GL5NAwHwkyAsP5jwQkTSyLA=; slave_user=gh_a98b7e0d1f3e; slave_sid=Tl92Q1VMeEVPNmpRZlhibkh0MkpOM0ZLQXp6eHRvSGJVbGRUOXpVSFN5OVJ3ajBtZHNZVjBRR21ubk1ueGRuemV6SWpLcF82cjRNUTk1NEMwcVpxSnlhMUhpMzJoaEZ3UVR3N0J2emFFS1AxTUx6OHJ4aUQxUTg4Slg4WElKaHY=; bizuin=3071518408");
                client.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + @"ProductImages\"+ productUnique + ".jpg");
            }
        }

        public string UploadImage(string imagePath, string imageName, HttpPostedFileBase file)
        {
            string url = string.Format(ImageUploadUrl, AccessToken, imageName);
            string jsonResponse = string.Empty;

            int fileLen = file.ContentLength;
            byte[] image = new byte[fileLen];

            file.InputStream.Read(image, 0, fileLen);

            Uri uri = new Uri(url, UriKind.Absolute);

            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                //jsonResponse = client.UploadFile(uri, imageName);
                byte[] bs = client.UploadData(uri, image);
                jsonResponse = System.Text.Encoding.UTF8.GetString(bs);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic result = js.Deserialize<dynamic>(jsonResponse);

            return result["image_url"];
        }

        
        public string UploadProduct(ProductJson prod)
        {
            string postData = JsonConvert.SerializeObject(prod);
            string url = string.Format(ProductUploadUrl, AccessToken);

            dynamic result = PostHttpData(url, postData);

            return result["product_id"];
        }

        //public static string ToJSON(this object obj)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(obj);
        //}

        //public static string ToJSON(this object obj, int recursionDepth)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    serializer.RecursionLimit = recursionDepth;
        //    return serializer.Serialize(obj);
        //}


        private string GetAccessToken()
        {
            string url = string.Format(AccessTokenUrl, Appid, Secret);
            dynamic token = GetHttpData(url);

            return token["access_token"];
        }

        private dynamic GetHttpData(string url)
        {
            string jsonResponse = string.Empty;
            WebClient client = new WebClient();
            Uri uri = new Uri(url, UriKind.Absolute);
            if (!client.IsBusy)
            {
                client.Encoding = System.Text.Encoding.UTF8;
                jsonResponse = client.DownloadString(uri);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic result = js.Deserialize<dynamic>(jsonResponse);

            return result;
        }

        private dynamic PostHttpData(string url, string postData)
        {
            string jsonResponse = string.Empty;
            
            Uri uri = new Uri(url, UriKind.Absolute);
            
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                jsonResponse = client.UploadString(uri,postData);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic result = js.Deserialize<dynamic>(jsonResponse);

            return result;
        }

    }
}