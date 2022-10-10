using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tess_API.Models
{
    public class OCRResult
    {
        /// <summary>
        /// 
        /// </summary>
        public double confidence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int[][] text_region { get; set; }
    }
}