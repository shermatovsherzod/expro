﻿using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class QuestionAnswerRepository : BaseCRUDRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<QuestionAnswer> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Question).ThenInclude(m => m.Creator);
        }
    }
}