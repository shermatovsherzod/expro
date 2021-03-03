using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class DocumentsCountVM
    {
        public int ArticleDocumentsCount { get; set; }
        public int SampleDocumentsCount { get; set; }
        public int PracticeDocumentsCount { get; set; }
    }

    public class DocumentsPurchasedCountVM
    {
        public int ArticleDocumentsCount { get; set; }
        public int SampleDocumentsCount { get; set; }
        public int PracticeDocumentsCount { get; set; }
    }

    public class ExpertDocumentsPurchasedCountVM : DocumentsPurchasedCountVM
    {
        public int AnsweredQuestionsCount { get; set; }
    }

    public class SimpleUserDocumentsPurchasedCountVM : DocumentsPurchasedCountVM
    {
        public int MyQuestionsCount { get; set; }
    }

    public class OtherItemsCountVM
    {
        public int CompaniesCount { get; set; }
        public int VacanciesCount { get; set; }
        public int ResumesCount { get; set; }
    }

    public class AdminSidebarItemsCountVM
    {
        public int SimpleUsersCount { get; set; }
        public int ExpertsCount { get; set; }

        public int QuestionsCount { get; set; }
        public int ArticleDocumentsCount { get; set; }
        public int SampleDocumentsCount { get; set; }
        public int PracticeDocumentsCount { get; set; }

        public int CompaniesCount { get; set; }
        public int VacanciesCount { get; set; }
        public int ResumesCount { get; set; }
    }
}
