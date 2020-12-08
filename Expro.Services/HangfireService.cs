using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Services
{
    public class HangfireService : IHangfireService
    {
        private readonly IDocumentService DocumentService;
        private readonly IDocumentAdminActionsService DocumentAdminActionsService;
        private readonly IQuestionDocumentAdminActionsService QuestionDocumentAdminActionsService;
        private readonly IUserBalanceService UserBalanceService;

        public HangfireService(
            IDocumentService documentService,
            IDocumentAdminActionsService documentAdminActionsService,
            IQuestionDocumentAdminActionsService questionDocumentAdminActionsService,
            IUserBalanceService userBalanceService)
        {
            DocumentService = documentService;
            DocumentAdminActionsService = documentAdminActionsService;
            QuestionDocumentAdminActionsService = questionDocumentAdminActionsService;
            UserBalanceService = userBalanceService;
        }

        public string CreateJobForDocumentRejectionDeadline(Document document)
        {
            string jobID = BackgroundJob.Schedule(() =>
                DocumentRejectionDeadlineReaches(document.ID),
                new DateTimeOffset(document.RejectionDeadline.Value));

            return jobID;
        }

        public void DocumentRejectionDeadlineReaches(int documentID)
        {
            //try
            //{
            Document document = DocumentService.GetByID(documentID);

            if (document.PriceType == DocumentPriceTypesEnum.Paid)
                UserBalanceService.ReplenishBalance(document.Creator, document.Price.Value);

            DocumentAdminActionsService.RejectionDeadlineReaches(document);
            //}
            //catch (Exception ex)
            //{ }
        }

        public string CreateJobForQuestionDocumentCompletionDeadline(Document document)
        {
            string jobID = BackgroundJob.Schedule(() =>
                QuestionDocumentCompletionDeadlineReaches(document.ID),
                new DateTimeOffset(document.QuestionCompletionDeadline.Value));

            return jobID;
        }

        public void QuestionDocumentCompletionDeadlineReaches(int documentID)
        {
            //try
            //{
            Document document = DocumentService.GetByID(documentID);

            if (document.PriceType == DocumentPriceTypesEnum.Paid)
                UserBalanceService.ReplenishBalance(document.Creator, document.Price.Value);

            QuestionDocumentAdminActionsService.CompletionDeadlineReaches(document);
            //}
            //catch (Exception ex)
            //{ }
        }

        public void CancelJob(string jobID)
        {
            if (!string.IsNullOrWhiteSpace(jobID))
            {
                //try
                //{
                    BackgroundJob.Delete(jobID);
                //}
                //catch
                //{ }
            }
        }

        //public void CreateTaskForConvertingVideo(int attachmentID, string userID)
        //{
        //    try
        //    {
        //        BackgroundJob.Enqueue(() => StartConvertingVideo(attachmentID, userID));
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //public void StartConvertingVideo(int attachmentID, string userID)
        //{
        //    try
        //    {
        //        Attachment attachment = AttachmentService.GetByID(attachmentID);
        //        string newVideoName = FileManagement.ConvertVideoToMp4(attachment.Path, attachment.GUID + "." + attachment.Extension);
        //        FileManagement.DeleteFile(attachment.Path + "/" + attachment.GUID + "." + attachment.Extension);

        //        string[] tmp = newVideoName.Split('.');
        //        attachment.GUID = tmp[0];
        //        attachment.Extension = tmp[1];

        //        AttachmentService.Update(attachment, userID);
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }
}
