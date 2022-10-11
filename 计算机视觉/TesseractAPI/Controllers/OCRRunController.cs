using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<string> ImageRecognition([FromBody] ImageUploadBody image_body)
        {
            string results = string.Empty;
            try
            {
                await Task.Run(() =>
                {
                    results = OCRHelper.RunTess(image_body);
                });
                await Task.Run(() => {
                    OCRHelper.WriteMessage(results);
                });
            }
            catch (Exception)
            {

                throw;
            }                       
            return results;
        }
    }
}