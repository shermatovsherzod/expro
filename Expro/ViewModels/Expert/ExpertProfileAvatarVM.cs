using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels.Expert
{
    public class ExpertShortInfoVM
    {
        public string ID { get; set; }
        public AttachmentDetailsVM Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ExpertApproved { get; set; }
        public bool IsOnline { get; set; }
        public string RegionStr { get; set; }
        public string CityStr { get; set; }

        public ExpertShortInfoVM() { }

        public ExpertShortInfoVM(ApplicationUser model) 
        {
            if (model == null)
                return;

            ID = model.Id;
            Avatar = new AttachmentDetailsVM(model.Avatar);
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            ExpertApproved = model.UserStatusID == (int)ExpertApproveStatusEnum.Approved ? true : false;
            IsOnline = model.IsOnline == true ? true : false;
            RegionStr = model.Region != null ? model.Region.Name : "";
            CityStr = model.City != null ? model.City.Name : model.CityOther;
        }
    }
}
