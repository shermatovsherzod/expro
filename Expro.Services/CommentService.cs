﻿using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.IO;
using System.Linq;

namespace Expro.Services
{
    public class CommentService : BaseAuthorableService<Comment>, ICommentService
    {
        public CommentService(ICommentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}