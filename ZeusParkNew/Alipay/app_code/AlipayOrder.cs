using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeusParkNew
{
    // should generate an unique id
    public class AlipayOrder
    {
        // get the unique id for the product
        // example:3c4ebc5f5f2c4edc
        // but we may only need to generate numbers
        public static string GetID() 
        { 
            // may create by date time and with product id from wechat?
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1,9999).ToString();
        }
    }
}