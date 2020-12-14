using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    public class QuestionCounterService : IQuestionCounterService
    {
        private IQuestionService QuestionService;

        public QuestionCounterService() { }

        public QuestionCounterService(IQuestionService questionService)
        {
            QuestionService = questionService;
        }

        public void IncrementNumberOfViews(Question model)
        {
            try
            {
                if (model == null)
                    return;

                int number = model.NumberOfViews;
                number++;
                model.NumberOfViews = number;

                QuestionService.Update(model);
            }
            catch
            { }
        }

        public void IncrementNumberOfPurchases(Question model)
        {
            try
            {
                if (model == null)
                    return;

                int number = model.NumberOfPurchases;
                number++;
                model.NumberOfPurchases = number;

                QuestionService.Update(model);
            }
            catch
            { }
        }
    }
}