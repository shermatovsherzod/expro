﻿using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IFeedbackSearchService
    {
        IQueryable<Feedback> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,           
            string authorID,
            string feedbackToUser
           );
    }
}