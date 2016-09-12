using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ZeusPark.Service.Model
{
    public class ProductJson
    {
        [JsonProperty("product_base")]
        public ProductJsonBase ProductBase {get;set;}

        [JsonProperty("sku_list")]
        public List<SkuJson> Sku { get; set; }

        [JsonProperty("delivery_info")]
        public ProductJosnDelivery Delivery { get; set; }
    }

    public class ProductJsonBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category_id")]
        public string CategoryID { get; set; }

        [JsonProperty("main_img")]
        public string MainImage { get; set; }

        [JsonProperty("img")]
        public string[] Images { get; set; }

        [JsonProperty("detail")]
        public List<ProductJosnDetail> Detail { get; set; }

    }

    public class SkuJson
    {
        [JsonProperty("sku_id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }

    public class ProductJosnDetail
    {
        
    }

    public class ProductJosnDetailText : ProductJosnDetail
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class ProductJosnDetailImg : ProductJosnDetail
    {
        [JsonProperty("img")]
        public string Image { get; set; }
    }

    public class ProductJosnDelivery
    {
        [JsonProperty("delivery_type")]
        public string Type { get; set; }
    }

}