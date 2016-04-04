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

        public AliService()
        {
            client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        }


        public void UploadImage(byte[] data, string name)
        {
            
            //try
            //{
                //string str = "a line of simple text";
                //byte[] binaryData = Encoding.ASCII.GetBytes(str);
                MemoryStream requestContent = new MemoryStream(data);
                //var metadata = new ObjectMetadata();
                //metadata.ContentType = "image/jpeg";
                client.PutObject("zeusparkweb", "prodpic/" + name + ".jpg", requestContent);
                //Console.WriteLine("Put object succeeded");
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine("Put object failed, {0}", ex.Message);
            //}
        }

    }
}
