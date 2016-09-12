using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class CommentVM
    {
        public long CommentID { get; set; }

        public int ProductID { get; set; }
        public string Content { get; set; }
        public string Reply { get; set; }
        public DateTime SubmitTime { get; set; }
        public string SubmitBy { get; set; }
        public int  Star { get; set; }
    }
}
