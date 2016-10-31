using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public static class EnumUtil
    {
        public static string GetOrderStatus(int status)
        {
            string value = string.Empty;
            switch (status)
            {
                case 0:
                    value = "订单取消";
                    break;
                case 1:
                    value = "未付款";
                    break;
                case 2:
                    value = "已付款";
                    break;
                case 3:
                    value = "已发货";
                    break;
                case 4:
                    value = "确认收货";
                    break;
            };

            return value;      
        }


    }
}
