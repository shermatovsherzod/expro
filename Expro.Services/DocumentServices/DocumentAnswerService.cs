using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Expro.Services
{
    public class DocumentAnswerService : BaseAuthorableService<DocumentAnswer>, IDocumentAnswerService
    {
        public DocumentAnswerService(IDocumentAnswerRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public bool DistributionIsCorrect(List<int> percentages)
        {
            return percentages.Sum() == 100;
        }

        public int CalculatePaidFee(int questionFee, int percentage)
        {
            return questionFee * percentage / 100;
        }
    }
}