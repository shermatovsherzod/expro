using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class City : BaseModelDropdownable
    {
        [ForeignKey("Region")]
        public int? RegionID { get; set; }
        public virtual Region Region { get; set; }
    }
}
