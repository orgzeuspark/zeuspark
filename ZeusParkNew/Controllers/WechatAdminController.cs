using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;
using ZeusPark.Web.Models;

namespace ZeusPark.Web.Controllers
{
    public class WechatAdminController : Controller
    {
        //
        // GET: /WechatAdmin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string code, string state)
        {
            WeChatProxy proxy = new WeChatProxy();
            string openid;
            string accesstoken = proxy.GetAccessTokenByCode(code, out openid);

            WeChatUserInfo info = proxy.GetUserInfoByTokenOpenId(accesstoken, openid);

            UserService service = new UserService();
            //if (!service.IsWeChatAccountExist(info.OpenId))
            //{
            //    service.CreateWeChatAccount(info.Name, info.OpenId, info.HeadImgUrl, info.UnionId);
            //}

            Session["UserId"] = info.Name;

            return RedirectToAction("Index", "Home");

        }

        //
        // GET: /WechatAdmin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WechatAdmin/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult RefreshGroups()
        {
               
            WeChatProxy proxy = new WeChatProxy();
            //string token = proxy.AccessToken;
            Collection<GroupVM> groupsVM = proxy.GetGroups();

            WechatDataImport import = new WechatDataImport();
            import.ImportGruops(groupsVM);

            return View("WechatAdmin");
        }

        public ActionResult ImportProductByGroup()
        {
            //List<int> groups = new List<int>() { 200512651, 200512652 };
            List<int> groups = new List<int>() {                    200707046                    
            };
            foreach (var gid in groups)
            {
                WeChatProxy proxy = new WeChatProxy();
                WechatDataImport import = new WechatDataImport();
                List<string> ids = proxy.GetProductListByGroup(gid);

                foreach (string id in ids)
                {
                    ProductVM prod = proxy.GetProductByID(id);
                    if (null == prod)
                    {
                        continue;
                    }
                    prod.Groups.Add(new GroupVM() { GroupID = gid });
                    import.ImportProduct(prod);
                }
            }


            return View("WechatAdmin");
        }

        public ActionResult DownloadPayImage()
        {


            WeChatProxy proxy = new WeChatProxy();

            WechatDataImport import = new WechatDataImport();
            var ids = import.GetAllProductsUnique();
            foreach (var id in ids)
            {
                //proxy.GetPayImage("pyKYTt_JmEfPCJylq8UG5Mm8MmF8");
                proxy.GetPayImage(id);
            }


            return View("WechatAdmin");
        }

        public ActionResult UploadImage()
        {
            WeChatProxy proxy = new WeChatProxy();
            //string value = proxy.UploadImage(@"E:\MyWork\ZeusPark\ZeusPark\ZeusPark.Web\Images\banner1.jpg", "banner1.jpg");

            return null;
        }

        public ActionResult GetImageList()
        {
            WeChatProxy proxy = new WeChatProxy();
            AliService service = new AliService();

            int count = 1830;
            for(int i = 988; i < count; i++)
            {
                string mediaId = string.Empty;
                string url = string.Empty;
                proxy.GetMaterailList(i, ref mediaId, ref url);
                if (!string.IsNullOrEmpty(url))
                {
                    url = url.Replace("https://mmbiz.qlogo.cn/mmbiz/", "");
                    url = url.Replace("?wx_fmt=jpeg", "");

                    var data = proxy.GetMaterial(mediaId);


                    service.UploadImage(data, url);
                }
                //string id = "XfcYcqm6BY3UyRjtlzODplHQIlkn-mMrHo7jtJAIHbk";

            }


            return null;
        }

        

        //
        // POST: /WechatAdmin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /WechatAdmin/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WechatAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /WechatAdmin/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WechatAdmin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
