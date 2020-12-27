using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface IRatingUpdateService : IBaseCRUDService<RatingUpdate>
    {
        void UpdateRatingForAllExperts();
    }
}