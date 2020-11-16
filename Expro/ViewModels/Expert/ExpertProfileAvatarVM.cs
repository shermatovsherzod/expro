using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels.Expert
{
    public class ExpertProfileAvatarVM
    {
        public string ID { get; set; }
        public AttachmentDetailsVM Avatar { get; set; }

        public ExpertProfileAvatarVM() { }

        public ExpertProfileAvatarVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

            ID = model.Id;
            Avatar = new AttachmentDetailsVM(model.Avatar);
        }
    }
}
