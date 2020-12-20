using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class DocumentLawArea
    {
        [ForeignKey("Document")]
        public int DocumentID { get; set; }
        public virtual Document Document { get; set; }

        [ForeignKey("LawArea")]
        public int LawAreaID { get; set; }
        public virtual LawArea LawArea { get; set; }
    }
}
