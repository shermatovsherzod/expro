using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Data.Repository.Interfaces
{
    public interface ICompanyRepository : IBaseCRUDRepository<Company>
    {
        IQueryable<Company> GetManyWithRelatedDataAsIQueryable();
    }
}