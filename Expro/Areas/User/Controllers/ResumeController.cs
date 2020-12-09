using System;
using System.Collections.Generic;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class ResumeController : BaseController
    {      
        private readonly IResumeService _resumeService;        
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;

        public ResumeController(         
              IResumeService resumeService,            
              IRegionService regionService,
              ICityService cityService
              )
        {     
            _resumeService = resumeService;           
            _regionService = regionService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["resumeListVM"] = new ResumeDetailsVM().GetListOfResumeDetailsVM(_resumeService.GetResumeByCreatorID(user.ID));
            return View();
        }

        public IActionResult Create()
        {
            GetResumeViewDataForCreate();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ResumeEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            GetResumeViewDataForCreate();
            if (ModelState.IsValid && user != null)
            {
                Resume model = new Resume();
                model.ResumeStatusID = (int)ResumeStatusEnum.WaitingForApproval;
                PropertyCopier.CopyTo(vmodel, model);

                _resumeService.Add(model, user.ID);

                return RedirectToAction("Index");
            }
            return View(vmodel);
        }

        public void GetResumeViewDataForCreate()
        {
            ViewData["regions"] = _regionService.GetAsSelectList();
            ViewData["cities"] = _cityService.GetAsSelectList();
        }


        public ActionResult Edit(int id)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (!_resumeService.ResumeBelongsToUser(id, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }

            ResumeEditVM vmodel = new ResumeEditVM(_resumeService.GetByID(id));
            GetResumeViewDataForEdit(vmodel);
            return View(vmodel);
        }

        [HttpPost]
        public ActionResult Edit(ResumeEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);

            if (!_resumeService.ResumeBelongsToUser(vmodel.ID, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }
            
            GetResumeViewDataForEdit(vmodel);
            
            if (ModelState.IsValid && user != null)
            {
                Resume model = _resumeService.GetActiveByID(vmodel.ID);
                model.ResumeStatusID = (int)ResumeStatusEnum.WaitingForApproval;
                PropertyCopier.CopyTo(vmodel, model);                          
                _resumeService.Update(model);

                return RedirectToAction("Index");
            }
            return View(vmodel);
        }

        public void GetResumeViewDataForEdit(ResumeEditVM vmodel)
        {
            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
        }

        public ActionResult Details(int id)
        {
            ResumeDetailsVM vmodel = new ResumeDetailsVM(_resumeService.GetByID(id));
            return View(vmodel);
        }
               
        public ActionResult Delete(int id)
        {
            ResumeDetailsVM vmodel = new ResumeDetailsVM(_resumeService.GetByID(id));
            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Delete(ResumeDetailsVM vmodel)
        {
            try
            {
                var model = _resumeService.GetByID(vmodel.ID);
                var user = accountUtil.GetCurrentUser(User);
                if (_resumeService.ResumeBelongsToUser(model, user.ID))
                {
                    _resumeService.DeletePermanently(model);
                    return RedirectToAction("Index");
                }
                throw new Exception("Что то пошло не так. Резюме не удален");
            }
            catch (Exception ex)
            {
                throw new Exception("Что то пошло не так. Резюме не удален");
            }
        }
    }
}