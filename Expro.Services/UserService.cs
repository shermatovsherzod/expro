using Expro.Common;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class UserService : BaseUserService<ApplicationUser>, IUserService
    {
        private IUserRepository _userRepository;
        protected AppConfiguration AppConfiguration { get; set; }

        public UserService(IUserRepository repository,
                           IUnitOfWork unitOfWork,
                           IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork)
        {
            if (settings != null)
                AppConfiguration = settings.Value;
            _userRepository = repository;
        }       

        public bool UserIsAllowedToWorkWithPaidMaterials(ApplicationUser user)
        {
            return (UserHasEnoughRatingToWorkWithPaidMaterials(user) && IsConfirmed(user));
        }

        public bool UserHasEnoughRatingToWorkWithPaidMaterials(ApplicationUser user)
        {
            int ratingTreshold = 0;

            try
            {
                ratingTreshold = AppConfiguration?.RatingThresholdForCreatingPaidDocuments ?? 0;
            }
            catch (Exception ex) { }

            return user.Rating >= ratingTreshold;
        }

        public bool IsConfirmed(ApplicationUser user)
        {
            return (user.UserStatusID == (int)ExpertApproveStatusEnum.Approved);
        }

        public IQueryable<ApplicationUser> GetManyWithRelatedDataAsIQueryable()
        {            
            return _userRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<ApplicationUser> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<ApplicationUser> GetAllExpertsForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.UserType == UserTypesEnum.Expert);
        }

        public IQueryable<ApplicationUser> GetAllSimpleUsersForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.UserType == UserTypesEnum.SimpleUser);
        }

        public IQueryable<ApplicationUser> GetAllApprovedExperts()
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.UserStatusID == (int)ExpertApproveStatusEnum.Approved && c.UserType == UserTypesEnum.Expert);
        }

        public ApplicationUser GetWithRelatedDataByID(string id)
        {
            return _userRepository.GetWithRelatedDataByID(id);
        }

        
        public IQueryable<ApplicationUser> GetAllExpertsAndSimpleUsers()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.UserType == UserTypesEnum.Expert
                    || m.UserType == UserTypesEnum.SimpleUser);
        }

        public IQueryable<ApplicationUser> GetTopExperts(int count)
        {
            return GetAllExpertsForAdmin()
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Points)
                .ThenByDescending(m => m.FeedbacksWrittenToThisExpert.Average(n => n.Stars))
                .Take(count);
        }

        private IQueryable<ApplicationUser> GetAdmins()
        {
            return GetAsIQueryable().Where(m => m.UserType == UserTypesEnum.Admin);
        }

        public List<string> GetAdminEmails()
        {
            return GetAdmins().Select(m => m.Email).ToList();
        }
    }
}