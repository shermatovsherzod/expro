using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class DocumentAnswerComment// : BaseModel
    {
        [ForeignKey("DocumentAnswer")]
        public int DocumentAnswerID { get; set; }
        public virtual DocumentAnswer DocumentAnswer { get; set; }

        [ForeignKey("Comment")]
        public int CommentID { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
