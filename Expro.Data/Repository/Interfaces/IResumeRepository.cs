﻿using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Data.Repository.Interfaces
{
    public interface IResumeRepository : IBaseCRUDRepository<Resume>
    {
        IQueryable<Resume> GetManyWithRelatedDataAsIQueryable();
    }
}