using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{    
    public class Document : BaseModelApprovableByAdmin
    {
        [ForeignKey("DocumentType")]
        public int DocumentTypeID { get; set; }
        public virtual DocumentType DocumentType { get; set; }

        [Required]
        [StringLength(1024)]
        public string Title { get; set; }

        [ForeignKey("Language")]
        public int? LanguageID { get; set; }
        public virtual Language Language { get; set; }

        public virtual ICollection<DocumentLawArea> DocumentLawAreas { get; set; }

        public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.text;

        [ForeignKey("Attachment")]
        public int? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

        public string Text { get; set; }

        public DocumentPriceTypesEnum PriceType { get; set; } = DocumentPriceTypesEnum.Free;
        public int? Price { get; set; } // in soums

        //[ForeignKey("DocumentStatus")]
        //public int DocumentStatusID { get; set; }
        //public virtual DocumentStatus DocumentStatus { get; set; }

        //public DateTime? DateSubmittedForApproval { get; set; }
        //public DateTime? DateApproved { get; set; }
        //public DateTime? DateRejected { get; set; }
        //public DateTime? RejectionDeadline { get; set; }
        //public string RejectionJobID { get; set; }

        public int NumberOfViews { get; set; }
        public int NumberOfPurchases { get; set; }

        public DateTime? QuestionCompletionDeadline { get; set; }
        public string QuestionCompletionJobID { get; set; }
        public bool? QuestionIsCompleted { get; set; }
        public bool? QuestionFeeIsDistributed { get; set; }
        public DateTime? DateQuestionCompleted { get; set; }

        [InverseProperty("Document")]
        public virtual ICollection<UserPurchasedDocument> UsersPurchasedThisDocument { get; set; }
        
        [InverseProperty("Document")]
        public virtual ICollection<DocumentLike> DocumentLikes { get; set; }
    }
}
