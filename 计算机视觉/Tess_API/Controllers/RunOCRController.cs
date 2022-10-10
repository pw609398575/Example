using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tess_API.Common;
using Tess_API.Models;

namespace Tess_API.Controllers
{
    public class RunOCRController : ApiController
    {
        [HttpPost]
        public string ImageUpload([FromBody] ImageUploadBody image_body)
        {
            string results = OCRHelper.RunTess(image_body);
            Task.Run(()=> {
                OCRHelper.WriteMessage(results);
            });            
            return results;
        }
    }
}