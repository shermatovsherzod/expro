using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expro.ViewModels
{
    public class HomePageVM
    {
        public HomePageStatsVM Stats { get; set; }

        public List<DocumentListItemForSiteVM> FeaturedSampleDocuments { get; set; }
        public List<DocumentListItemForSiteVM> FeaturedPracticeDocuments { get; set; }
        public List<QuestionListItemForSiteVM> FeaturedQuestions { get; set; }

        public List<ExpertTopInfoVM> TopExperts { get; set; }
    }

    public class HomePageStatsVM
    {
        public int QuestionsCount { get; set; }
        public int ArticlesCount { get; set; }
        public int SampleDocumentsCount { get; set; }
        public int ExpertsCount { get; set; }
    }
}