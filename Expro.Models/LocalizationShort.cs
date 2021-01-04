using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expro.Models
{
    public class LocalizationShort : BaseModel
    {
        [Required]
        [StringLength(256)]
        public string TextRu { get; set; }

        [Required]
        [StringLength(256)]
        public string TextUz { get; set; }
    }
}
