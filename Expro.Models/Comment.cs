using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Comment : BaseModelAuthorable
    {
        public string Text { get; set; }

        [ForeignKey("Attachment")]
        public int? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

        [InverseProperty("Comment")]
        public virtual ICollection<DocumentComment> DocumentComments { get; set; }

        [InverseProperty("Comment")]
        public virtual ICollection<CommentLike> Likes { get; set; }
    }
}
