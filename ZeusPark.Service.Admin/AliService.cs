using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Admin
{
    public class AliService
    {
        const string accessKeyId = "qJCufUmCN2tlPyJ0";
        const string accessKeySecret = "znBiBfOnChVrO3bxIouqm8KWZl0lyf";
        const string endpoint = "http://oss-cn-shanghai.aliyuncs.com";
        OssClient client;
        const string bucketName = "zeusparkweb";

        public AliService()
        {
            client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        }


        public void UploadTempImage(byte[] data, string key)
        {
            using (MemoryStream requestContent = new MemoryStream(data))
            {
                client.PutObject(bucketName, "tempprodpic/" + key + ".jpg", requestContent);
            }
            
        }

        public void MoveImageFromTemp(string key)
        {
            string sourcekey = "tempprodpic/" + key + ".jpg";
            string destkey = "newprodpic/" + key + ".jpg";

            client.CopyObject(new CopyObjectRequest(bucketName, sourcekey, bucketName,destkey));
            client.DeleteObject(bucketName, sourcekey);
        }




    }
}
