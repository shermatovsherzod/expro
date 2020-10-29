﻿using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IEducationService : IBaseAuthorableService<Education>
    {
        IQueryable<Education> GetListByUserID(string userID);
    }
}