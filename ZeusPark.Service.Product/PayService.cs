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

                }

            }

            return discountVM;
        }


    }
}
