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

        [Required]
        public string Text { get; set; }

        public string CommentType { get; set; }

        public Comment ToModel()
        {
            Comment comment = new Comment()
            {
                Text = this.Text
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

        public CommentDetailsVM() { }

        public CommentDetailsVM(Comment model)
            : base(model)
        {
            if (model == null)
                return;

            Text = model.Text;
            Attachment = new AttachmentDetailsVM(model.Attachment);
            Author = new AppUserVM(model.Creator);
            DatePublished = DateTimeUtils.ConvertToString(model.DateCreated);
            PositiveLikesCount = model.Likes.Count(m => m.IsPositive);
            NegativeLikesCount = model.Likes.Count(m => !m.IsPositive);
        }
    }

    //public class CommentLikeVM : BaseVM
    //{


    //    public CommentLikeVM() { }

    //    public CommentLikeVM(CommentLike model)
    //        : base(model)
    //    {
    //        if (model == null)
    //            return;


    //    }
    //}
}
