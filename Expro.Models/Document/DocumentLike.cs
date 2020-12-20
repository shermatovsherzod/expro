using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class DocumentLike
    {
        [ForeignKey("Document")]
        public int DocumentID { get; set; }
        public virtual Document Document { get; set; }

        [ForeignKey("Like")]
        public int LikeID { get; set; }
        public virtual Like Like { get; set; }
    }
}
