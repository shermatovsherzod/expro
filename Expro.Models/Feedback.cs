using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{    
    public class Feedback : BaseModelAuthorable
    {
        [Required]
        public int Stars { get; set; }

        [StringLength(400)]
        public string FeedbackText { get; set; }

        [ForeignKey("ToUser")]
        public string FeedbakToUser { get; set; }
        public virtual ApplicationUser ToUser { get; set; }

        [ForeignKey("FeedbackStatus")]
        public int FeedbackStatusID { get; set; }
        public virtual FeedbackStatus FeedbackStatus { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateRejected { get; set; }

        [StringLength(256)]
        public string RejectedReasonText { get; set; }
    }
}
