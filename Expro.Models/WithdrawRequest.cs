using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class WithdrawRequest : BaseModelAuthorable
    {
        public int Amount { get; set; }

        [ForeignKey("Status")]
        public int StatusID { get; set; }
        public virtual WithdrawRequestStatus Status { get; set; }

        public DateTime? DateCompleted { get; set; }
    }
}
