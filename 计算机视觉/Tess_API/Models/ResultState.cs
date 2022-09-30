using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tess_API.Models
{
    public class ResultState
    {
        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string status { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string msg { get; set; }

        public List<List<OCRResult>> results { get; set; }
    }
}