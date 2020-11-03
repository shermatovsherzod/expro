using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class ExpertDocumentsViewComponent : ViewComponent
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IDocumentService _documentService;

        public ExpertDocumentsViewComponent(
            UserManager<ApplicationUser> userManager,
            IDocumentService documentService)
        {
            _userManager = userManager;
            _documentService = documentService;
        }

        public IViewComponentResult Invoke()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            DocumentsCountVM vmodel = new DocumentsCountVM();

            vmodel.SampleDocumentCount = _documentService.GetAsIQueryable().Count(c => c.CreatedBy == currentUser.Id && c.DocumentTypeID == 2); //Намунавий хужжат 2
            vmodel.PracticDocumentCount = _documentService.GetAsIQueryable().Count(c => c.CreatedBy == currentUser.Id && c.DocumentTypeID == 3); //Амалиёт 3

            return View("ExpertDocuments", vmodel);
        }

        
    }
}
