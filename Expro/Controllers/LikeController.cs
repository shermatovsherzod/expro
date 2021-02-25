using System;
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
    //[Authorize]
    public class LikeController : BaseController
    {
        private readonly ILikeService _likeService;
        private readonly IDocumentService DocumentService;
        private readonly IQuestionAnswerService QuestionAnswerService;
        private readonly IUserRatingService _userRatingService;
        private readonly UserManager<ApplicationUser> _userManager;

        //private readonly IHostingEnvironment _env;

        public LikeController(
            ILikeService likeService,
            IDocumentService documentService,
            IQuestionAnswerService questionAnswerService,
            //IHostingEnvironment env,
            IUserRatingService userRatingService,
            UserManager<ApplicationUser> userManager,
            ILogger<AttachmentController> logger)
        {
            _likeService = likeService;
            DocumentService = documentService;
            QuestionAnswerService = questionAnswerService;
            //_env = env;
            _userRatingService = userRatingService;
            _userManager = userManager;
            _logger = logger;
        }

        //ajax
        [HttpPost]
        public IActionResult Save([FromBody] LikeCreateVM likeCreateVM)
        {
            try
            {
                var curUser = accountUtil.GetCurrentUser(User);

                Like like = likeCreateVM.ToModel();
                _likeService.Add(like, curUser.ID);
                
                AttachLike(like, likeCreateVM.ObjectID, likeCreateVM.LikeType);

                return Ok(new { id = like.ID });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        private void AttachLike(Like like, object id, string likeType)
        {
            string modelIDString = id.ToString();
            int modelIDInt = 0;
            int.TryParse(modelIDString, out modelIDInt);

            if (modelIDInt > 0)
            {
                if (likeType.Equals(Constants.LikeTypes.DOCUMENT))
                {
                    var model = DocumentService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.DocumentLikes.Add(new DocumentLike()
                        {
                            Like = like
                        });

                        int points = 0;
                        if (DocumentService.IsFree(model))
                            points = Constants.PointsFor.DOCUMENT.ARTICLE.FREE_LIKE;
                        else
                            points = Constants.PointsFor.DOCUMENT.ARTICLE.PAID_LIKE;

                        var expert = model.Creator;
                        _userRatingService.AddPointsToUser(points, expert);

                        DocumentService.Update(model);
                    }
                }
                if (likeType.Equals(Constants.LikeTypes.QUESTION_ANSWER))
                {
                    var model = QuestionAnswerService.GetByID(modelIDInt);
                    if (model != null)
                    {
                        model.QuestionAnswerLikes.Add(new QuestionAnswerLike()
                        {
                            Like = like
                        });

                        if (like.IsPositive)
                        {
                            int points = Constants.PointsFor.QUESTION_ANSWER_LIKE;
                            var expert = model.Creator;
                            _userRatingService.AddPointsToUser(points, expert);
                        }

                        QuestionAnswerService.Update(model);
                    }
                }
            }
            else
            {
                //if (commentType.Equals(Constants.FileTypes.USER_AVATAR))
                //{
                //    var user = await _userManager.FindByIdAsync(modelIDString);
                //    if (user != null)
                //    {
                //        user.Avatar = attachment;
                //        await _userManager.UpdateAsync(user);
                //    }
                //}
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
        //[HttpPost]
        //public IActionResult Delete(int fileID, int? objID, string fileType)
        //{
        //    try
        //    {
        //        var curUser = accountUtil.GetCurrentUser(User);

        //        var model = CommentService.GetByID(fileID);
        //        if (model == null)
        //            throw new Exception("Файл не найден в базе");

        //        if (!objID.HasValue)
        //        {
        //            CommentService.DeletePermanently(model);
        //        }
        //        else if (objID > 0 && !string.IsNullOrWhiteSpace(fileType))
        //        {
        //            DetachFromObj(model, objID.Value, fileType, curUser.ID);
        //            CommentService.Delete(model, curUser.ID);
        //        }
        //        else
        //            throw new Exception("Некоторые входящие данные неверные");

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return CustomBadRequest(ex);
        //    }
        //}

        //private void DetachFromObj(Attachment attachment, int objID, string fileType, string curUserID)
        //{
        //    if (fileType.Equals(Constants.FileTypes.DOCUMENT))
        //    {
        //        var obj = DocumentService.GetActiveByID(objID);
        //        if (obj == null)
        //            throw new Exception("Объект не найден");

        //        if (!DocumentService.AttachedFileIsAllowedToBeDeleted(obj))
        //            throw new Exception("У Вас недостаточно прав для удаления этого файла");

        //        obj.Attachment = null;
        //        DocumentService.Update(obj, curUserID);
        //    }
        //    //else if () ...
        //}
    }
}