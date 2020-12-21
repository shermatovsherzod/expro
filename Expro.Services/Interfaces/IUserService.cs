using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<ApplicationUser> GetAsIQueryable();
        ApplicationUser GetByID(string id);
    }
}