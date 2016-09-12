using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeusPark.Service.Model
{
    public class WeChatUserInfo
    {
        public string OpenId { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        public string HeadImgUrl { get; set; }

        public string UnionId { get; set; }
    }
}