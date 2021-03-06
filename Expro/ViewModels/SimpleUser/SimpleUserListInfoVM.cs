using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class SimpleUserListInfoVM
    {
        public SimpleUserListInfoVM()
        {

        }
        public string Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Емейл")]
        public string Email { get; set; }
        
        public AttachmentDetailsVM Avatar { get; set; }

        public bool IsOnline { get; set; }

        public SimpleUserListInfoVM(ApplicationUser model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Avatar = new AttachmentDetailsVM(model.Avatar);
            Email = model.Email;
            IsOnline = model.IsOnline ?? false;          
        }

        public List<SimpleUserListInfoVM> GetSimpleUserListInfoVM(IQueryable<ApplicationUser> models)
        {
            if (models == null)
                return new List<SimpleUserListInfoVM>();

            return models.Select(model => new SimpleUserListInfoVM
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Avatar = new AttachmentDetailsVM(model.Avatar),
                Email = model.Email,
                IsOnline = model.IsOnline ?? false,               
            }).ToList();
        }
    }
}
