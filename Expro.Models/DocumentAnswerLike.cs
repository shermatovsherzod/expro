using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class DocumentAnswerLike : BaseModel
    {
        [ForeignKey("DocumentAnswer")]
        public int DocumentAnswerID { get; set; }
        public virtual DocumentAnswer DocumentAnswer { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsPositive { get; set; }
    }
}
