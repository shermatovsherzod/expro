using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class UserStatus : BaseModelDropdownable
    {
        [ForeignKey("NameShort")]
        public int NameID { get; set; }

        public virtual LocalizationShort NameShort { get; set; }
    }
}
