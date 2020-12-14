using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class QuestionLawArea
    {
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }

        [ForeignKey("LawArea")]
        public int LawAreaID { get; set; }
        public virtual LawArea LawArea { get; set; }
    }
}
