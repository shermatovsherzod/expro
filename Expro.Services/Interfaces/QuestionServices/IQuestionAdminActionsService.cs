using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IQuestionAdminActionsService : IBaseApprovableByAdminService<Question>
    {
        void CompletionDeadlineReaches(Question Question);
    }
}