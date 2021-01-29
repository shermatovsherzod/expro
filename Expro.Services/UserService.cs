using Expro.Common;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
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
    }
}