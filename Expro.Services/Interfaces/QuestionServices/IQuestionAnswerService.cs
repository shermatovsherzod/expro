using Expro.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IQuestionAnswerService : IBaseAuthorableService<QuestionAnswer>
    {
        bool DistributionIsCorrect(List<int> percentages);
        int CalculatePaidFee(int questionFee, int percentage);
        //void AddLike(QuestionAnswer questionAnswer, string userID, bool isPositive);
        IQueryable<QuestionAnswer> GetManyPaidByAnswerer(string answererID);
    }
}