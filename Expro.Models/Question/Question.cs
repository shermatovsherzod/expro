using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{    
    public class Question : BaseModelApprovableByAdmin
    {
        [Required]
        [StringLength(1024)]
        public string Title { get; set; }

        [ForeignKey("LawAreaParent")]
        public int? LawAreaParentID { get; set; }
        public virtual LawArea LawAreaParent { get; set; }

        public virtual ICollection<QuestionLawArea> QuestionLawAreas { get; set; }

        [ForeignKey("Attachment")]
        public int? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

        public string Text { get; set; }

        public DocumentPriceTypesEnum PriceType { get; set; } = DocumentPriceTypesEnum.Free;
        public int? Price { get; set; } // in soums

        public int NumberOfViews { get; set; }
        public int NumberOfPurchases { get; set; }

        public DateTime? QuestionCompletionDeadline { get; set; }
        public string QuestionCompletionJobID { get; set; }
        public bool? QuestionIsCompleted { get; set; }
        public bool? QuestionFeeIsDistributed { get; set; }
        public DateTime? DateQuestionCompleted { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<QuestionAnswer> Answers { get; set; }
    }
}
