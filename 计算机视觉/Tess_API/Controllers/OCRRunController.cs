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
    public class OCRRunController : ApiController
    {
        [HttpPost]
        public async Task<string> ImageRecognition([FromBody] ImageUploadBody image_body)
        {
            string results = string.Empty;
            await Task.Run(() =>
            {
                results = OCRHelper.RunTess(image_body);
            });
            await Task.Run(() => {
                OCRHelper.WriteMessage(results);
            });
            return results;
        }
    }
}