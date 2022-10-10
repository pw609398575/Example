using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tess_API.Models
{
    /// <summary>
    /// 图片主体
    /// </summary>
    public class ImageUploadBody
    {
        public List<string> Images { get; set; }
    }
}