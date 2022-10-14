using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tess_API.Common;
using Tess_API.Models;

namespace Tess_API.Controllers
{
    /// <summary>
    /// OCR启动入口
    /// </summary>
    public class OCRRunController : ApiController
    {
        /// <summary>
        /// 图形文字识别
        /// </summary>
        /// <param name="image_body"></param>
        /// <returns></returns>
        [HttpPost]
        public string ImageRecognition([FromBody] ImageUploadBody image_body)
        {
            Task<string> task = Task.Run<string>(() =>
            {
                return OCRHelper.RunTess(image_body);
            });
            return task.Result;
        }
    }
}