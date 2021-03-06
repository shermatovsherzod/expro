﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expro.Controllers
{
    [Authorize]
    public class AttachmentController : BaseController
    {
        private readonly IAttachmentService AttachmentService;
        private readonly IDocumentService DocumentService;
        private readonly IQuestionService QuestionService;
        private readonly IQuestionAnswerService QuestionAnswerService;
        private readonly ICommentService CommentService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly IExpertEducationService _expertEducationService;

        //private readonly IHostingEnvironment _env;

        public AttachmentController(
            IAttachmentService attachmentService,
            IDocumentService documentService,
            IQuestionService questionService,
            IQuestionAnswerService questionAnswerService,
            ICommentService commentService,
            ICompanyService companyService,
            //IHostingEnvironment env,
            ILogger<AttachmentController> logger,
            IUserService userService,
            IExpertEducationService expertEducationService)
        {
            AttachmentService = attachmentService;
            DocumentService = documentService;
            QuestionService = questionService;
            QuestionAnswerService = questionAnswerService;
            CommentService = commentService;
            _companyService = companyService;
            //_env = env;
            _logger = logger;
            _userService = userService;
            _expertEducationService = expertEducationService;
        }

        //ajax
        [HttpPost]
        public IActionResult Save([FromBody] UploadFileVM uploadFileVM)
        {
            try
            {
                var curUser = accountUtil.GetCurrentUser(User);

                Attachment attachment = uploadFileVM.ToModel();
                AttachmentService.Add(attachment, curUser.ID);

                if (uploadFileVM.ModelID != null)
                    AttachFile(attachment, uploadFileVM.ModelID, uploadFileVM.FileType);

                //if (uploadFileVM.ModelID.HasValue && uploadFileVM.ModelID.Value > 0)
                //    AttachFile(attachment, uploadFileVM.ModelID.Value, uploadFileVM.FileType, curUser.ID);

                return Ok(new { id = attachment.ID });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        private void AttachFile(Attachment attachment, object id, string fileType)
        {
            string modelIDString = id.ToString();
            int modelIDInt = 0;
            int.TryParse(modelIDString, out modelIDInt);

            if (modelIDInt > 0)
            {
                if (fileType.Equals(Constants.FileTypes.DOCUMENT))
                {
                    var model = DocumentService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.Attachment = attachment;
                        DocumentService.Update(model);
                    }
                }
                else if (fileType.Equals(Constants.FileTypes.QUESTION))
                {
                    var model = QuestionService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.Attachment = attachment;
                        QuestionService.Update(model);
                    }
                }
                else if (fileType.Equals(Constants.FileTypes.QUESTION_ANSWER))
                {
                    var model = QuestionAnswerService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.Attachment = attachment;
                        QuestionAnswerService.Update(model);
                    }
                }
                else if (fileType.Equals(Constants.FileTypes.COMMENT))
                {
                    var model = CommentService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.Attachment = attachment;
                        CommentService.Update(model);
                    }
                }
                else if (fileType.Equals(Constants.FileTypes.COMPANY))
                {
                    var model = _companyService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.Logo = attachment;
                        _companyService.Update(model);
                    }
                }
                else if (fileType.Equals(Constants.FileTypes.EDUCATION))
                {
                    var model = _expertEducationService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.Diploma = attachment;
                        _expertEducationService.Update(model);
                    }
                }
            }
            else
            {
                if (fileType.Equals(Constants.FileTypes.USER_AVATAR))
                {
                    var user = _userService.GetByID(modelIDString);
                    if (user != null)
                    {
                        user.Avatar = attachment;
                        _userService.Update(user);
                    }
                }
            }
            
            //else if (fileType.Equals(Constants.FileTypes.TALENT_AVATAR))
            //{
            //    var model = TalentService.GetByID(id);
            //    if (model != null)
            //    {
            //        model.Avatar = attachment;
            //        TalentService.Update(model, curUserID);
            //    }
            //}

            //else if () ...
        }

        //[HttpPost]
        //[RequestSizeLimit(200000000)]
        //public IActionResult Upload(List<IFormFile> files, int? id, string fileType)
        //{
        //    var curUser = accountUtil.GetCurrentUser(User);

        //    IFormFile file = files[0];
        //    if (file != null)
        //    {
        //        Attachment attachment = new Attachment()
        //        {
        //            GUID = Guid.NewGuid().ToString(),
        //            Filename = file.FileName,
        //            Path = AppData.Configuration.UploadsPath,
        //            Extension = file.FileName.Split('.')[1],
        //            Size = file.Length,
        //            MimeType = file.ContentType
        //        };

        //        string rootPath = _env.ContentRootPath;

        //        string path = AppData.Configuration.UploadsPath;
        //        if (fileType.Equals(Constants.FileTypes.VIDEO_REQUEST_VIDEO))
        //        {
        //            string videosRelativePath = "/Videos";
        //            path += videosRelativePath;
        //            attachment.Path += videosRelativePath;
        //        }

        //        path += "/" + attachment.GUID + "." + file.FileName.Split('.')[1];
        //        path = path.Replace('/', '\\');

        //        string target = rootPath + path;

        //        using (var stream = new FileStream(target, FileMode.Create))
        //        {
        //            file.CopyTo(stream);

        //            AttachmentService.Add(attachment, curUser.ID);

        //            if (id.HasValue && id.Value > 0)
        //                AttachFile(attachment, id.Value, fileType, curUser.ID);
        //        }

        //        return Ok(new AttachmentDetailsVM(attachment));
        //    }

        //    return BadRequest();
        //}

        //ajax
        [HttpPost]
        public IActionResult Delete(int fileID, int? objID, string fileType)
        {
            try
            {
                var curUser = accountUtil.GetCurrentUser(User);

                var model = AttachmentService.GetByID(fileID);
                if (model == null)
                    throw new Exception("Файл не найден в базе");

                if (!objID.HasValue)
                {
                    AttachmentService.DeletePermanently(model);
                }
                else if (objID > 0 && !string.IsNullOrWhiteSpace(fileType))
                {
                    DetachFromObj(model, objID.Value, fileType, curUser.ID);
                    AttachmentService.Delete(model, curUser.ID);
                }
                else
                    throw new Exception("Некоторые входящие данные неверные");

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        private void DetachFromObj(Attachment attachment, int objID, string fileType, string curUserID)
        {
            if (fileType.Equals(Constants.FileTypes.DOCUMENT))
            {
                var obj = DocumentService.GetActiveByID(objID);
                if (obj == null)
                    throw new Exception("Объект не найден");

                if (!DocumentService.AttachedFileIsAllowedToBeDeleted(obj))
                    throw new Exception("У Вас недостаточно прав для удаления этого файла");

                obj.Attachment = null;
                DocumentService.Update(obj, curUserID);
            }
            //else if () ...
        }

        //uncomment when needed
        //[HttpPost]
        //public JsonResult UploadMultiple(List<IFormFile> files)
        //{
        //    return Json(new { });
        //}
    }
}