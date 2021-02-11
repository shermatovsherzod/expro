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
    public class ExpertListInfoVM
    {
        public ExpertListInfoVM()
        {

        }
        public string Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Статус")]
        public int UserStatusID { get; set; }

        [Display(Name = "Статус")]
        public string UserStatusStr { get; set; }

        [Display(Name = "Емейл")]
        public string Email { get; set; }

        public string AboutMe { get; set; }

        [Display(Name = "Балл")]
        public int Points { get; set; }

        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }

        [Display(Name = "Дата последнего обновления рейтинга")]
        public string DateRatingLastUpdated { get; set; }

        public AttachmentDetailsVM Avatar { get; set; }

        public bool IsOnline { get; set; }

        public ExpertListInfoVM(ApplicationUser model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Avatar = new AttachmentDetailsVM(model.Avatar);
            UserStatusID = model.UserStatusID;
            UserStatusStr = model.UserStatus.Name;
            Email = model.Email;
            IsOnline = model.IsOnline ?? false;
            Points = model.Points;
            Rating = model.Rating;
            DateRatingLastUpdated = model.DateRatingLastUpdated != null ? DateTimeUtils.ConvertToString(
                model.DateRatingLastUpdated,
                AppData.Configuration.DateTimeViewStringFormat) : "";
            AboutMe = model.AboutMe != null ? model.AboutMe : "";
        }

        public List<ExpertListInfoVM> GetExpertListInfoVM(IQueryable<ApplicationUser> models)
        {
            if (models == null)
                return new List<ExpertListInfoVM>();

            return models.Select(model => new ExpertListInfoVM
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Avatar = new AttachmentDetailsVM(model.Avatar),
                UserStatusID = model.UserStatusID,
                UserStatusStr = model.UserStatus.Name,
                Email = model.Email,
                IsOnline = model.IsOnline ?? false,
                Points = model.Points,
                Rating = model.Rating,
                DateRatingLastUpdated = model.DateRatingLastUpdated != null ? DateTimeUtils.ConvertToString(
                model.DateRatingLastUpdated,
                AppData.Configuration.DateTimeViewStringFormat) : "",
                AboutMe = model.AboutMe != null ? model.AboutMe : ""
            }).ToList();
        }
    }
}
