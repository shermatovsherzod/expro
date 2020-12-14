using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class QuestionAnswerLike : BaseModel
    {
        [ForeignKey("QuestionAnswer")]
        public int QuestionAnswerID { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsPositive { get; set; }
    }
}
