using Expro.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace Expro.Services.Interfaces
{
    public interface IQuestionAnswerService : IBaseAuthorableService<QuestionAnswer>
    {
        bool DistributionIsCorrect(List<int> percentages);
        int CalculatePaidFee(int questionFee, int percentage);
    }
}