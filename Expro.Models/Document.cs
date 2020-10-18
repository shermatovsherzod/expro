using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Document : BaseModelAuthorable
    {
        [ForeignKey("DocumentType")]
        public int DocumentTypeID { get; set; }
        public virtual DocumentType DocumentType { get; set; }

        [Required]
        [StringLength(1024)]
        public string Title { get; set; }

        [ForeignKey("Language")]
        public int LanguageID { get; set; }
        public virtual Language Language { get; set; }

        [ForeignKey("Attachment")]
        public int? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

        public string Text { get; set; }

        public int? Price { get; set; } // in soums

        [ForeignKey("DocumentStatus")]
        public int DocumentStatusID { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateRejected { get; set; }

        public string CancellationJobID { get; set; }

        public int NumberOfViews { get; set; }
        public int NumberOfPurchases { get; set; }
    }
}
