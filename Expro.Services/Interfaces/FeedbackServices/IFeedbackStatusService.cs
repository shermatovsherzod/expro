﻿using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{  
    public interface IFeedbackStatusService
    {
        List<SelectListItem> GetAsSelectList();
    }
}