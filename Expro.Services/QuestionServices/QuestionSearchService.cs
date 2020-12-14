﻿using Expro.Data.Infrastructure;
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
    public class QuestionSearchService : IQuestionSearchService
    {
        protected IQuestionService QuestionService;

        public QuestionSearchService() { }

        public QuestionSearchService(IQuestionService questionService)
        {
            QuestionService = questionService;
        }

        public IQueryable<Question> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,
            DocumentPriceTypesEnum? priceType,
            string authorID,
            string purchasedUserID,
            int[] lawAreas)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<Question> documents;

                if (curUserType.HasValue)
                {
                    //if (!string.IsNullOrWhiteSpace(purchasedUserID))
                    //    documents = DocumentService.GetAllPurchasedByUser(purchasedUserID);
                    //else if (curUserType == UserTypesEnum.Admin)
                    //    documents = DocumentService.GetAllForAdmin();
                    //else if (curUserType == UserTypesEnum.Expert)
                    //    documents = DocumentService.GetAllByCreator(authorID);
                    //else if (curUserType == UserTypesEnum.SimpleUser)
                    //    documents = DocumentService.GetAllPurchasedByUser(purchasedUserID);
                    //else
                    //    documents = DocumentService.GetAllApproved();

                    if (curUserType == UserTypesEnum.Admin)
                        documents = QuestionService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.SimpleUser)
                        documents = QuestionService.GetAllByCreator(authorID);
                    else
                        documents = QuestionService.GetAllApproved();
                }
                else
                    documents = QuestionService.GetAllApproved();

                recordsTotal = documents.Count();

                documents = ApplyFilters(documents, lawAreas, statusID, priceType);

                recordsFiltered = documents.Count();

                documents = ApplyOrder(documents, start, length);

                return documents;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<Question>().AsQueryable();
            }
        }

        protected IQueryable<Question> ApplyFilters(
            IQueryable<Question> questions,
            int[] lawAreas,
            int? statusID,
            DocumentPriceTypesEnum? priceType)
        {
            if (lawAreas != null && lawAreas.Length > 0)
            {
                questions = questions
                    .Where(m => m.QuestionLawAreas
                        .Select(n => n.LawAreaID)
                        .Any(n => lawAreas.Contains(n)));
            }

            if (statusID.HasValue)
            {
                if (statusID.Value == (int)DocumentStatusesEnum.QuestionCompleted)
                {
                    questions = questions
                        .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved
                            && m.QuestionIsCompleted == true);
                }
                else if (statusID.Value == (int)DocumentStatusesEnum.QuestionCompletedWithFeeDistribution)
                {
                    questions = questions
                        .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved
                            && m.QuestionIsCompleted == true
                            && m.QuestionFeeIsDistributed == true);
                }
                else
                    questions = questions.Where(m => m.DocumentStatusID == statusID.Value);
            }
                
            if (priceType.HasValue)
                questions = questions.Where(m => m.PriceType == priceType.Value);

            return questions;
        }

        protected IQueryable<Question> ApplyOrder(
            IQueryable<Question> documents,
            int? start,
            int? length)
        {
            documents = documents.OrderByDescending(m => m.DateModified);
            if (start.HasValue && start.Value > 0)
                documents = documents.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                documents = documents.Take(length.Value);

            return documents;
        }
    }
}