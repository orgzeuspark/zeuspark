using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ZeusPark.Service.Admin;
using ZeusPark.Service.Model;
using ZeusPark.Service.Product;

namespace ZeusPark.Web.Controllers
{

    public class DiscountController : ApiController
    {


        public DiscountVM Get(string codeid, string total)
        {
            try
            {
                if (string.IsNullOrEmpty(codeid))
                {
                    return new DiscountVM()
                    {
                        IsValid = false,
                        Message = "优惠码格式错误"
                    };
                }

                codeid = codeid.Trim();
                var totalFloat = Convert.ToDecimal(total);

                if (codeid.Length == 10)
                {

                    PayService service = new PayService();
                    var disc = service.GetDiscountByCode(codeid);

                    if (null != disc)
                    {
                        
                        if (totalFloat > disc.ConditionNum)
                        {
                            if (disc.EndDate != null && disc.EndDate.Value > DateTime.Now.Date)
                            {
                                disc.IsValid = false;
                                disc.Message = "优惠券已经过期";
                            }
                            else if (disc.Quantity > 0)
                            {
                                disc.IsValid = false;
                                disc.Message = "优惠券数额已经用完";
                            }
                            else if (disc.Status != 1)
                            {
                                disc.IsValid = false;
                                disc.Message = "优惠券已经失效";
                            }
                            else
                            {
                                disc.IsValid = true;
                            }


                        }
                        else
                        {
                            disc.IsValid = false;
                            disc.Message = "不满足减免优惠条件";
                        }

                    }
                    else
                    {
                        disc.IsValid = false;
                        disc.Message = "优惠码错误";
                    }

                    return disc;
                }
                else if (codeid.Length == 16)
                {
                    PayService service = new PayService();
                    var disc = service.GetDiscountByCode(codeid);
                    if (null != disc)
                    {
                        if (disc.DiscountNum > totalFloat)
                        {
                            disc.DiscountNum = totalFloat;
                        }

                        if (disc.EndDate != null && disc.EndDate.Value > DateTime.Now.Date)
                        {
                            disc.IsValid = false;
                            disc.Message = "优惠券已经过期";
                        }
                        else if (disc.Quantity > 0)
                        {
                            disc.IsValid = false;
                            disc.Message = "优惠券已经被使用";
                        }
                        else if (disc.Status != 1)
                        {
                            disc.IsValid = false;
                            disc.Message = "优惠券已经失效";
                        }
                        else
                        {
                            disc.IsValid = true;
                        }

                    }
                    else
                    {
                        disc.IsValid = false;
                        disc.Message = "优惠码错误";
                    }
                    return disc;
                }
                else
                {
                    return new DiscountVM()
                    {
                        IsValid = false,
                        Message = "优惠码格式错误"
                    };
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }



    }




}