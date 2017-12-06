using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LFC.Utility.Convert
{
    public static class ConvertHelper
    {
        public static byte[] Object2Bytes(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);
            return serializedResult;
        } ///
        public static byte[] GetFileData(this IFormFile file)
        {
            System.IO.Stream fs = file.OpenReadStream(); //打开文件流
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            fs.Seek(0, System.IO.SeekOrigin.Begin);
            return bytes;
        }
        //public Image ReturnPhoto(byte[] streamByte)
        //{
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
        //    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
        //    return img;
        //}
    }
}
