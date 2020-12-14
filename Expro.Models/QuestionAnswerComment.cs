using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class QuestionAnswerComment
    {
        [ForeignKey("QuestionAnswer")]
        public int QuestionAnswerID { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }

        [ForeignKey("Comment")]
        public int CommentID { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
