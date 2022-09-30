using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
            string results = string.Empty;
            Thread thread = new Thread(() => OCRHelper.RunTess(image_body, out results));
            thread.Start();

            int count = 0;
            while (results == "")
            {
                count++;
                Thread.Sleep(50);
                if (count > 100)
                {
                    break;
                }
            }
            OCRHelper.WriteMessage(results);
            return results;
        }
    }
}