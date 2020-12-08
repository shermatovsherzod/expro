using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common.Utilities;
using Expro.Models;

namespace Expro.ViewModels
{
    public class CommentCreateVM
    {
        public int? ObjectID { get; set; }
        public int? ParentID { get; set; }

        [Required]
        [Display(Name = "Текст комментария")]
        public string Text { get; set; }

        public string CommentType { get; set; }

        public Comment ToModel()
        {
            Comment comment = new Comment()
            {
                Text = this.Text,
                ParentID = this.ParentID
            };

            return comment;
        }
    }

    public class CommentDetailsVM : BaseVM
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

        public int DepthLevel { get; set; }
        public int? ParentID { get; set; }

        public CommentDetailsVM() { }

        public CommentDetailsVM(Comment model)
            : base(model)
        {
            if (model == null)
                return;

            Text = model.Text;
            ParentID = model.ParentID;
            Attachment = new AttachmentDetailsVM(model.Attachment);
            Author = new AppUserVM(model.Creator);
            DatePublished = DateTimeUtils.ConvertToString(model.DateCreated);
            PositiveLikesCount = model.Likes.Count(m => m.IsPositive);
            NegativeLikesCount = model.Likes.Count - PositiveLikesCount;
        }
    }
}
