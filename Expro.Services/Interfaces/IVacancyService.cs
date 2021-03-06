﻿using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IVacancyService : IBaseAuthorableService<Vacancy>
    {
        IQueryable<Vacancy> GetVacancyByCreatorID(string userID);
        bool VacancyBelongsToUser(Vacancy model, string userID);
        bool VacancyBelongsToUser(int modelID, string userID);
        IQueryable<Vacancy> GetAllForAdmin();
        IQueryable<Vacancy> GetAllApproved();
        void SubmitForApproval(Vacancy entity, string userID);
    }
}