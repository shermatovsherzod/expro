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
        public string FeedbakToUserFullName { get; set; }

        public FeedbackDetailsVM(Feedback model)
        {
            if (model == null)
                return;
            ID = model.ID;
            Stars = model.Stars;
            FeedbackText = model.FeedbackText;
            Status = model.FeedbackStatusID;
            FeedbakToUserFullName = model.ToUser.FirstName + " " + model.ToUser.LastName;
        }

        public List<FeedbackDetailsVM> GetListOfFeedbackDetailsVM(IQueryable<Feedback> models)
        {
            if (models == null)
                return new List<FeedbackDetailsVM>();

            return models.Select(s => new FeedbackDetailsVM
            {
                ID = s.ID,
                Stars = s.Stars,
                FeedbackText = s.FeedbackText,
                FeedbakToUserFullName = s.ToUser.FirstName + " " + s.ToUser.LastName,
                Status = s.FeedbackStatusID
            }).ToList();
        }
    }
}
