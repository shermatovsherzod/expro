using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Like : BaseModelAuthorable
    {
        public bool IsPositive { get; set; }

        public virtual ICollection<DocumentLike> DocumentLikes { get; set; }
        public virtual ICollection<QuestionAnswerLike> QuestionAnswerLikes { get; set; }
    }
}
