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

        public void DeleteProductByID(int productId)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var skus = entity.skuinfoes.Where(x => x.ProductID == productId);
                entity.skuinfoes.RemoveRange(skus);

                var group = entity.productgroups.Where(x => x.ProductID == productId);
                entity.productgroups.RemoveRange(group);

                var prod = entity.products.FirstOrDefault(x => x.ProductID == productId);
                entity.products.Remove(prod);

                entity.SaveChanges();
            }
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
                    if (string.IsNullOrEmpty(prod.MainImageUrl2))
                    {
                        prodVM.MainImageUrl2 = prod.MainImageUrl;
                    }
                    else
                    {
                        prodVM.MainImageUrl2 = prod.MainImageUrl2;
                    }
                    if (string.IsNullOrEmpty(prod.MainImageUrl3))
                    {
                        prodVM.MainImageUrl3 = prod.MainImageUrl;
                    }
                    else
                    {
                        prodVM.MainImageUrl3 = prod.MainImageUrl3;
                    }
                    prodVM.ProductUnique = prod.ProductUnique;
                    prodVM.DetailHtml = prod.DetailHtml;
                    prodVM.Description = prod.Description;
                    prodVM.Label = prod.Label;
                    var pImages = prod.productimages.ToList();
                    foreach (var img in pImages)
                    {
                        prodVM.Images.Add(new ImageVM() { ImageUrl = img.ImageUrl });
                    }
                    var pSkuInfos = prod.skuinfoes.OrderBy(x => x.InfoID).ToList();
                    foreach (var sku in pSkuInfos)
                    {
                        if (pSkuInfos.Count == 1 && string.IsNullOrEmpty(sku.Size))
                        {
                            sku.Size = "统一规格";
                        }

                        prodVM.SkuInfoList.Add(new SkuInfoVM()
                        {
                            InfoID = sku.InfoID,
                            OrgPrice = sku.OrgPrice,
                            Price = sku.Price,
                            Quantity = sku.Quantity.Value,
                            IconUrl = sku.IconUrl,
                            Size = sku.Size,
                            Color = sku.Color
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
                            where g1.GroupID == groupId && p1.Deleted == false && p1.Status == 1
                            orderby p1.CreateTime descending
                            select p1;

                foreach (var prod in query)
                {
                    prodGroupVM.Products.Add(new ProductVM()
                        {
                            MainImageUrl = prod.MainImageUrl,
                            Name = prod.Name,
                            ProductID = prod.ProductID,
                            DisplaySinglePrice = (prod.skuinfoes.First().Price/100).ToString(),
                            DisplaySingleOrgPrice = (prod.skuinfoes.First().OrgPrice / 100).ToString(),
                            Label = string.IsNullOrEmpty(prod.Label) ? "其他" : prod.Label
                    });
                }
                prodGroupVM.GroupName = entity.groupvalues.First(x => x.GroupID == groupId).GroupName;
            }

            return prodGroupVM;
        }

        public List<ProductVM> GetAllProductsWithMainFields()
        {
            List<ProductVM> prodlist = new List<ProductVM>();

            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var query = from p in entity.products
                            orderby p.CreateTime descending
                            select p;
                foreach (var prod in query)
                {
                    prodlist.Add(new ProductVM()
                    {
                        Name = prod.Name,
                        MainImageUrl = prod.MainImageUrl,
                        ProductID = prod.ProductID,
                        Status = prod.Status,
                        DisplayStatus = prod.Status == 1 ? "已上架" : "未上架"
                    });
                }
            }

            return prodlist;
        }

        public void UpdateProductStatus(int productId, int status)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var prod = entity.products.FirstOrDefault(x => x.ProductID == productId);

                if (null != prod)
                {
                    prod.Status = status;
                    entity.SaveChanges();
                }
            }
        }


        


    }
}
