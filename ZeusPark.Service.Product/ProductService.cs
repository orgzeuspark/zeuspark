using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Product
{
    public class ProductService
    {
        public ProductService()
        {

        }

        public ProductVM GetProductByID(int productId)
        {
            ProductVM prodVM = new ProductVM();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var prod = entity.products.FirstOrDefault(x => x.ProductID == productId);
                if (null != prod)
                {
                    prodVM.ProductID = prod.ProductID;
                    prodVM.Name = prod.Name;
                    prodVM.MainImageUrl = prod.MainImageUrl;
                    prodVM.ProductUnique = prod.ProductUnique;
                    prodVM.DetailHtml = prod.DetailHtml;
                    var pImages = prod.productimages.ToList();
                    foreach (var img in pImages)
                    {
                        prodVM.Images.Add(new ImageVM() { ImageUrl = img.ImageUrl });
                    }
                    var pSkuInfos = prod.skuinfoes.ToList();
                    foreach (var sku in pSkuInfos)
                    {
                        prodVM.SkuInfoList.Add(new SkuInfoVM()
                        {
                            OrgPrice = sku.OrgPrice,
                            Price = sku.Price,
                            Quantity = sku.Quantity.Value,
                            IconUrl = sku.IconUrl
                        });
                    }
                    var sale = prod.productsaleinfoes.FirstOrDefault();
                    if (null != sale)
                    {
                        prodVM.SaleInfo.PayImageUrl = sale.PayImageUrl;
                    }
                    
                    

                }

            }

            return prodVM;
        }

        public ProductGroupVM GetProductGroupList(int groupId)
        {
            ProductGroupVM prodGroupVM = new ProductGroupVM();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var query = from p1 in entity.products
                            join g1 in entity.productgroups on p1.ProductID equals g1.ProductID
                            where g1.GroupID == groupId
                            select p1;

                foreach (var prod in query)
                {
                    prodGroupVM.Products.Add(new ProductVM()
                        {
                            MainImageUrl = prod.MainImageUrl,
                            Name = prod.Name,
                            ProductID = prod.ProductID,
                            DisplaySinglePrice = (prod.skuinfoes.First().Price/100).ToString()
                        });
                }
                prodGroupVM.GroupName = entity.groupvalues.First(x => x.GroupID == groupId).GroupName;
            }

            return prodGroupVM;
        }

    }
}
