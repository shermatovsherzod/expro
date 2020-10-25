using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Region : BaseModelDropdownable
    {
        [InverseProperty("Region")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
