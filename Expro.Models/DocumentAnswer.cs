using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Expro.Models
{
    public class DocumentAnswer : BaseModelAuthorable
    {
        public string Text { get; set; }

        [ForeignKey("Attachment")]
        public int? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

        [ForeignKey("Document")]
        public int DocumentID { get; set; }
        public virtual Document Document { get; set; }

        public int? PaidFee { get; set; }

        [InverseProperty("DocumentAnswer")]
        public virtual ICollection<DocumentAnswerComment> Comments { get; set; }

        [InverseProperty("DocumentAnswer")]
        public virtual ICollection<DocumentAnswerLike> Likes { get; set; }

    }
}
