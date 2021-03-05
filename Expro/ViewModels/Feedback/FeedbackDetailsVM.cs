using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class FeedbackDetailsVM
    {
        public FeedbackDetailsVM() { }
        public int ID { get; set; }

        [Display(Name = "Статус")]
        public int Status { get; set; }

        [Display(Name = "Звезда")]
        public int Stars { get; set; }

        [Display(Name = "Отзыв")]
        public string FeedbackText { get; set; }

        [Display(Name = "Отзыв для")]
        public string FeedbackToUserFullName { get; set; }

        [Display(Name = "Отзыв от")]
        public string FeedbackCreatedUserFullName { get; set; }

        [Display(Name = "Дата")]
        public string FeedbackDateCreated { get; set; }
        public AttachmentDetailsVM FeedbackCreatedUserAvatar { get; set; }

        public FeedbackDetailsVM(Feedback model)
        {
            if (model == null)
                return;
            ID = model.ID;
            Stars = model.Stars;
            FeedbackText = model.FeedbackText != null ? model.FeedbackText : "";
            Status = model.FeedbackStatusID;
            FeedbackToUserFullName = model.ToUser.FirstName + " " + model.ToUser.LastName;
            FeedbackCreatedUserFullName = model.Creator.FirstName + " " + model.Creator.LastName;
            FeedbackDateCreated = model.DateCreated != DateTime.MinValue ? DateTimeUtils.ConvertToString(model.DateCreated, AppData.Configuration.DateViewStringFormat) : "";
            FeedbackCreatedUserAvatar = new AttachmentDetailsVM(model.Creator.Avatar);


        }

        public List<FeedbackDetailsVM> GetListOfFeedbackDetailsVM(IQueryable<Feedback> models)
        {
            if (models == null)
                return new List<FeedbackDetailsVM>();

            return models.Select(s => new FeedbackDetailsVM
            {
                ID = s.ID,
                Stars = s.Stars,
                FeedbackText = s.FeedbackText != null ? s.FeedbackText : "",
                FeedbackToUserFullName = s.ToUser.FirstName + " " + s.ToUser.LastName,
                Status = s.FeedbackStatusID,
                FeedbackCreatedUserFullName = s.Creator.FirstName + " " + s.Creator.LastName,
                FeedbackDateCreated = s.DateCreated != DateTime.MinValue ? DateTimeUtils.ConvertToString(s.DateCreated, AppData.Configuration.DateViewStringFormat) : "",
                FeedbackCreatedUserAvatar = new AttachmentDetailsVM(s.Creator.Avatar)
            }).ToList();
        }
    }
}
