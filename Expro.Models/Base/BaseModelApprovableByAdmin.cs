using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public abstract class BaseModelApprovableByAdmin : BaseModelAuthorable
    {
        [ForeignKey("DocumentStatus")]
        public int DocumentStatusID { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DatePublished { get; set; }
        public DateTime? DateRejected { get; set; }
        public DateTime? RejectionDeadline { get; set; }
        public string RejectionJobID { get; set; }
    }
}
