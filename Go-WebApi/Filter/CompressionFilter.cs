using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using Ionic.Zlib;

namespace Go_WebApi.Filter
{
    public class CompressionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 压缩返回结果
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var content = actionExecutedContext.Response.Content;
            var bytes = content.ReadAsByteArrayAsync().Result;

            var zlibbedContent = bytes == null ? new byte[0] : CompressionHelper.DeflateByte(bytes);
            actionExecutedContext.Response.Content = new ByteArrayContent(zlibbedContent);
            actionExecutedContext.Response.Content.Headers.Remove("Content-Type");
            actionExecutedContext.Response.Content.Headers.Add("Content-Encoding", "deflate");
            actionExecutedContext.Response.Content.Headers.Add("Content-Type", "application/json");

            base.OnActionExecuted(actionExecutedContext);
        }
    }

    public class CompressionHelper
    {
        public static byte[] DeflateByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }
            using (var output = new MemoryStream())
            {
                using (var compressor = new DeflateStream(output, CompressionMode.Compress, CompressionLevel.BestSpeed))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }
        }
    }
}