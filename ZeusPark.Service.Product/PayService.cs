using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Product
{
    public class PayService
    {
        public PayService()
        {

        }

        public DiscountVM GetDiscountByCode(string codeID)
        {
            DiscountVM discountVM = new DiscountVM();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var disc = entity.discounts.FirstOrDefault(x => x.CodeID == codeID && x.Status == 1);
                if (null != disc)
                {
                    discountVM.CodeID = disc.CodeID;
                    discountVM.Title = disc.Title;
                    discountVM.ConditionNum = disc.ConditionNum;
                    discountVM.DiscountNum = disc.DiscountNum.Value;
                    discountVM.Status = disc.Status;
                    discountVM.EndDate = disc.EndDate;
                    discountVM.Quantity = disc.Quantity;
                }

            }

            return discountVM;
        }

        public void FinishPayment(string orderid)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var order = entity.orders.FirstOrDefault(x => x.OrderUnique == orderid);
                if (null != order)
                {
                    order.Status = 2; //payed
                    order.LastUpdateTime = DateTime.Now;

                    var prodids = order.ProdIds;
                    if (!string.IsNullOrEmpty(prodids))
                    {
                        var ids = prodids.Split(';');
                        foreach(var id in ids)
                        {
                            if (!string.IsNullOrEmpty(id))
                            {
                                int prodid = 0;
                                if (Int32.TryParse(id, out prodid))
                                {
                                    var prod = entity.products.FirstOrDefault(x => x.ProductID == prodid);
                                    if (null != prod)
                                    {
                                        if (prod.BuyLimit.HasValue)
                                        {
                                            prod.BuyLimit = (prod.BuyLimit.Value + 1);
                                        }
                                        else
                                        {
                                            prod.BuyLimit = 1;
                                        }
                                    }

                                }
                                
                            }
                        }
                    }

                    var discountCode = order.DiscountCode;
                    if (!string.IsNullOrEmpty(discountCode))
                    {
                        var code = entity.discounts.FirstOrDefault(x => x.CodeID == discountCode);
                        if (null != code)
                        {
                            if (code.Quantity > 0)
                            {
                                code.Quantity = code.Quantity - 1;
                            }
                        }
                    }


                    entity.SaveChanges();
                }
            }
        }

    }
}
