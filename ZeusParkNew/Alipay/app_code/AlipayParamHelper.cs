using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeusParkNew
{
    public class AlipayParamHelper
    {
        public static string BuildParamters(string strSubject, string strFee)
        {
            // id
            string tradeNo = AlipayOrder.GetID();
            // subject
            string subject = strSubject; //"测试订单";

            // fee
            string totalFee = strFee; 
            //string totalFee = "0.01";

            //支付类型
            string payment_type = "1";

            //服务器异步通知页面路径
            string notify_url = "http://www.zeuspark.com/Alipay/notify_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = "http://www.zeuspark.com/Alipay/return_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //商品展示地址
            string show_url = "http://www.zeuspark.com";
            //需以http://开头的完整路径，例如：http://www.zeuspark.com/myorder.html


            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", AlipayConfig.Partner);
            sParaTemp.Add("seller_email", AlipayConfig.Seller_email);
            sParaTemp.Add("_input_charset", AlipayConfig.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("out_trade_no", tradeNo);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", totalFee);
            sParaTemp.Add("body", "");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", AlipaySubmit.Query_timestamp());
            sParaTemp.Add("exter_invoke_ip", "");

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }
    }
}