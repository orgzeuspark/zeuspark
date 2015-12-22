using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class ProdUploadHistory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string MainImg { get; set; }

        public string ProdImg { get; set; }

        public double Price { get; set; }

        public int UploadUser { get; set; }

        public DateTime UploadTime { get; set; }

        public string ProductUnique { get; set; }

        public int GroupId { get; set; }
    }
}
