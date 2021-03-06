﻿using Expro.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Expro.Services.Interfaces
{
    public interface ICommentService : IBaseAuthorableService<Comment>
    {
    }
}