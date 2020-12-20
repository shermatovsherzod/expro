using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class QuestionAnswerLike
    {
        [ForeignKey("QuestionAnswer")]
        public int QuestionAnswerID { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }

        [ForeignKey("Like")]
        public int LikeID { get; set; }
        public virtual Like Like { get; set; }
    }
}