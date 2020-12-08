using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common.Utilities;
using Expro.Models;

namespace Expro.ViewModels
{
    public class QuestionAnswerCreateVM
    {
        public int DocumentID { get; set; }

        [Required]
        [Display(Name = "Текст ответа")]
        public string Text { get; set; }

        public DocumentAnswer ToModel()
        {
            DocumentAnswer answer = new DocumentAnswer()
            {
                Text = this.Text,
                DocumentID = this.DocumentID
            };

            return answer;
        }
    }

    public class QuestionAnswerDetailsVM : BaseVM
    {
        public string Text { get; set; }

        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        [Display(Name = "Дата")]
        public string DatePublished { get; set; }

        public int PositiveLikesCount { get; set; }
        public int NegativeLikesCount { get; set; }

        public List<CommentDetailsVM> Comments { get; set; }

        public QuestionAnswerDetailsVM() { }

        public QuestionAnswerDetailsVM(DocumentAnswer model)
            : base(model)
        {
            if (model == null)
                return;

            Text = model.Text;
            Attachment = new AttachmentDetailsVM(model.Attachment);
            Author = new AppUserVM(model.Creator);
            DatePublished = DateTimeUtils.ConvertToString(model.DateCreated);
            PositiveLikesCount = model.Likes.Count(m => m.IsPositive);
            NegativeLikesCount = model.Likes.Count - PositiveLikesCount;
            
            Comments = new List<CommentDetailsVM>();
            //var allComments = model.Comments.Select(m => m.Comment).ToList();
            //var rootComments = allComments.Where(m => !m.ParentID.HasValue);
            //Stack<CommentDetailsVM> commentsStack = new Stack<CommentDetailsVM>();
            




            foreach (var item in model.Comments)
            {
                Comments.Add(new CommentDetailsVM(item.Comment));
            }
        }
    }

    public class QuestionFeeDistributionVM
    {
        public int QuestionDocumentID { get; set; }
        public List<DistributedQuestionAnswerVM> Answers { get; set; }
    }

    public class DistributedQuestionAnswerVM
    {
        public int AnswerID { get; set; }
        public int Percentage { get; set; }
    }
}
