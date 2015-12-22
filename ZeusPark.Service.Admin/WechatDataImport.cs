using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Admin
{
    public class WechatDataImport
    {


        public WechatDataImport()
        {

        }

        public void ImportGruops(Collection<GroupVM> groupvms)
        {
            using(zeusparkEntities entity = new zeusparkEntities())
            {

                var groupsInDB = entity.groupvalues.ToList(); // single DB query

                foreach (GroupVM groupvm in groupvms)
                {
                    groupvalue value = new groupvalue();
                    value.Deleted = false;
                    value.GroupID = groupvm.GroupID;
                    value.GroupName = groupvm.GroupName;

                    var oneGroup = groupsInDB.SingleOrDefault(c => c.GroupID == groupvm.GroupID); // runs in memory
                    if (oneGroup != null)
                    {
                        entity.groupvalues.Attach(value);
                        entity.Entry(value).State = EntityState.Modified;
                    }
                    else
                    {
                        entity.groupvalues.Add(value);
                    }
                }


                entity.SaveChanges();
            }
            
        }

        public void ImportProduct(ProductVM productVM)
        {
            bool isUpdate = false;
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                product prod = entity.products.FirstOrDefault(x => x.ProductUnique == productVM.ProductUnique);
                if (null == prod)
                {
                    prod = new product();
                    prod.CreateTime = DateTime.Now;
                    prod.ProductUnique = productVM.ProductUnique;
                }
                else
                {
                    isUpdate = true;
                }

                prod.BuyLimit = productVM.BuyLimit;                
                prod.Deleted = false;

                if (isUpdate)
                {
                    if (null != prod.deliveryinfo && null != productVM.DeliveryInfo)
                    {
                        prod.deliveryinfo.DeliveryType = productVM.DeliveryInfo.DeliveryType;
                        prod.deliveryinfo.TemplateId = productVM.DeliveryInfo.TemplateId;
                    }
                }
                else
                {
                    if (null != productVM.DeliveryInfo)
                    {
                        prod.deliveryinfo = new deliveryinfo()
                        {
                            DeliveryType = productVM.DeliveryInfo.DeliveryType,
                            TemplateId = productVM.DeliveryInfo.TemplateId
                        };
                    }
                    else
                    {
                        prod.DeliveryInfoID = null;
                    }
                }

                if (!isUpdate)
                {
                    prod.productsaleinfoes.Add(new productsaleinfo()
                    {
                        ProductUnique = productVM.ProductUnique
                    });
                }

            
                prod.DetailHtml = productVM.DetailHtml;
                prod.ExtentionAttributeID = null;
                prod.LastUpdateTime = DateTime.Now;
                prod.MainImageUrl = productVM.MainImageUrl;
                prod.Name = productVM.Name;
                prod.Status = productVM.Status;

                if (!isUpdate)
                {
                    entity.products.Add(prod);
                }

                if (!isUpdate)
                {
                    foreach (ImageVM imgVM in productVM.Images)
                    {
                        prod.productimages.Add(new productimage() { ImageUrl = imgVM.ImageUrl });
                    }

                    foreach (SkuInfoVM skuVM in productVM.SkuInfoList)
                    {
                        prod.skuinfoes.Add(new skuinfo()
                        {
                            IconUrl = skuVM.IconUrl,
                            OrgPrice = skuVM.OrgPrice,
                            Price = skuVM.Price,
                            ProductCode = skuVM.ProductCode,
                            Quantity = skuVM.Quantity,
                            SkuID = skuVM.skuId
                        });
                    }
                }

                

                entity.SaveChanges();

                int groupId = productVM.Groups[0].GroupID;
                var prodGroup = entity.productgroups.FirstOrDefault(x => x.GroupID == groupId && x.ProductID == prod.ProductID);
                if (prodGroup == null)
                {
                    entity.productgroups.Add(new productgroup()
                        {
                            ProductID = prod.ProductID,
                            GroupID = groupId,
                            ProductUnique = prod.ProductUnique
                        });
                    entity.SaveChanges();
                }

            }
        }

        public List<string> GetAllProductsUnique()
        {
            List<string> uniqueIds = new List<string>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var ids = entity.products.Select(x => x.ProductUnique).ToList();
                foreach (var id in ids)
                {
                    uniqueIds.Add(id);
                }
            }

            return uniqueIds;
        }

    }
}
