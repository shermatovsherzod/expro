using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Expro.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.ViewModels.SimpleUser
{
    public class SimpleUserShortInfoVM
    {
        public SimpleUserShortInfoVM() { }
        public string ID { get; set; }
        public AttachmentDetailsVM Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public SimpleUserShortInfoVM(ApplicationUser model)
        {
            if (model == null)
                return;

            ID = model.Id;
            Avatar = new AttachmentDetailsVM(model.Avatar);
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
        }
    }
}
