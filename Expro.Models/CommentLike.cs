using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class CommentLike : BaseModel
    {
        [ForeignKey("Comment")]
        public int CommentID { get; set; }
        public virtual Comment Comment { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsPositive { get; set; }
    }
}
