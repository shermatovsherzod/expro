using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Country : BaseModelDropdownable
    {
        [InverseProperty("Country")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
