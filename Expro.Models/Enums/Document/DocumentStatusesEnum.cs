using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Models.Enums
{
    public enum DocumentStatusesEnum
    {
        Pending = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Rejected = 4,
        QuestionCompletedWithFeeDistribution = 100,
        QuestionCompleted = 200,
        QuestionOpen = 300,
    }
}
