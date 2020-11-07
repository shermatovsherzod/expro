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
    public class DocumentCounterService : IDocumentCounterService
    {
        private IDocumentService DocumentService;

        public DocumentCounterService() { }

        public DocumentCounterService(IDocumentService documentService)
        {
            DocumentService = documentService;
        }

        public void IncrementNumberOfViews(Document model)
        {
            try
            {
                if (model == null)
                    return;

                int number = model.NumberOfViews;
                number++;
                model.NumberOfViews = number;

                DocumentService.Update(model);
            }
            catch
            { }
        }

        public void IncrementNumberOfPurchases(Document model)
        {
            try
            {
                if (model == null)
                    return;

                int number = model.NumberOfPurchases;
                number++;
                model.NumberOfPurchases = number;

                DocumentService.Update(model);
            }
            catch
            { }
        }
    }
}