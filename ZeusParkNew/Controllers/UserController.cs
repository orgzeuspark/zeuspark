using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;
using ZeusPark.Web.Models;

namespace ZeusParkNew.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserAccount userAccount)
        {
            UserService service = new UserService();
            service.CreateAccount(userAccount);

            return View("UploadArticle");
        }

        public ActionResult Login()
        {
            ViewBag.IsValid = true;
            return View();
        }

        public ActionResult LoginSubmit(FormCollection collection)
        {
            string name = collection["name"].ToString();
            string pass = collection["pass"].ToString();
            string username = string.Empty;
            UserService ser = new UserService();
            var result = ser.IsValidAccount(name, pass, out username);

            if (result > 0)
            {
                Session["UserId"] = username;
                Session["LoginUserId"] = result;
                ViewBag.IsValid = true;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.IsValid = false;
            ViewBag.LoginFail = "Password is not correct!";
            return View("Login");
        }

        public ActionResult loginwx(string code, string state)
        {
            WeChatProxy proxy = new WeChatProxy();
            string openid;
            string accesstoken = proxy.GetAccessTokenByCode(code, out openid);

            WeChatUserInfo info = proxy.GetUserInfoByTokenOpenId(accesstoken, openid);

            UserService service = new UserService();
            int userid = 0;
            if (!service.IsWeChatAccountExist(info.OpenId, out userid))
            {
                userid = service.CreateWeChatAccount(info.Name, info.OpenId, info.HeadImgUrl, info.UnionId);
            }

            Session["UserId"] = info.Name;
            Session["LoginUserId"] = userid;

            return RedirectToAction("Index", "Home");

        }

        public ActionResult UploadArticle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddArt(FormCollection form, IEnumerable<HttpPostedFileBase> file1, IEnumerable<HttpPostedFileBase> file2)
        {
            string mainImgPath = file1.First().FileName;
            string mainImg = Path.GetFileName(mainImgPath);
            string otherImgPath = file2.First().FileName;
            string otherImg = Path.GetFileName(otherImgPath);

            WeChatProxy proxy = new WeChatProxy();

            string mainImgId = proxy.UploadImage(mainImgPath, mainImg, file1.First());
            string otherImgId = proxy.UploadImage(otherImgPath, otherImg, file2.First());

            string name = form["title"].ToString();
            string text = form["zpms"].ToString();
            double price = Convert.ToDouble(form["price"].ToString());
            string categoryId = "536894968";

            ProductJson prod = new ProductJson();
            prod.ProductBase = new ProductJsonBase();
            prod.ProductBase.CategoryID = categoryId;
            prod.ProductBase.MainImage = mainImgId;
            prod.ProductBase.Name = name;
            prod.ProductBase.Images = new string[] { otherImgId };
            prod.ProductBase.Detail = new List<ProductJosnDetail>();
            prod.ProductBase.Detail.Add(new ProductJosnDetailText() { Text = text });
            prod.ProductBase.Detail.Add(new ProductJosnDetailImg() { Image = otherImgId });
            prod.Delivery = new ProductJosnDelivery();
            //prod.Sku = new List<SkuJson>();
            //prod.Sku.Add(new SkuJson() { Id = "", Price = "10000", Quantity = "1" });
            prod.Delivery.Type = "0";

            string uid = proxy.UploadProduct(prod);

            UserService service = new UserService();
            ProdUploadHistory history = new ProdUploadHistory();
            history.CategoryId = categoryId;
            history.Description = text;
            history.MainImg = mainImgId;
            history.Name = name;
            history.Price = price;
            history.ProdImg = otherImg;
            history.ProductUnique = uid;
            history.UploadTime = DateTime.Now;
            history.UploadUser = int.Parse(Session["LoginUserID"] as string);

            service.AddUploadHistory(history);

            return View("AddSuccess");
        }

        public ActionResult GetUploadHistory()
        {
            UserService service = new UserService();
            var list = service.GetHistoryByUserId(1);

            return new JsonResult() { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult UploadHistory()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

    }
}
