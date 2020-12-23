using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Expro.Models
{
    public class QuestionAnswer : BaseModelAuthorable
    {
        [Required]
        public string Text { get; set; }

        [ForeignKey("Attachment")]
        public int? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }

        public int? PaidFee { get; set; }

        [InverseProperty("QuestionAnswer")]
        public virtual ICollection<QuestionAnswerComment> Comments { get; set; }

        [InverseProperty("QuestionAnswer")]
        public virtual ICollection<QuestionAnswerLike> QuestionAnswerLikes { get; set; }
    }
}
