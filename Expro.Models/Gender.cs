using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Gender : BaseModelDropdownable
    {
        //[InverseProperty("Gender")]
        //public ICollection<ApplicationUser> Users { get; set; }
    }
}
