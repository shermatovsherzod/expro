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

        public FeedbackDetailsVM(Feedback model)
        {
            if (model == null)
                return;
            ID = model.ID;
           
            Status = model.FeedbackStatusID;
        }

        public List<FeedbackDetailsVM> GetListOfFeedbackDetailsVM(IQueryable<Feedback> models)
        {
            if (models == null)
                return new List<FeedbackDetailsVM>();

            return models.Select(s => new FeedbackDetailsVM
            {
                ID = s.ID,
               
                Status=s.FeedbackStatusID
            }).ToList();
        }
    }
}
