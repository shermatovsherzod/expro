﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels.Expert;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class ProfileUserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;       
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;

        public ProfileUserController(
            UserManager<ApplicationUser> userManager,          
            IRegionService regionService,
            ICityService cityService,
            IGenderService genderService,
            ICountryService countryService         
            )
        {
            _userManager = userManager;         
            _regionService = regionService;
            _cityService = cityService;
            _genderService = genderService;
            _countryService = countryService;           
        }

        public IActionResult Index()
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

            ViewData["country"] = _countryService.GetAsSelectList();          
            ViewData["regions"] = _regionService.GetAsSelectListOne(currentUser.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(currentUser.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(currentUser.GenderID);

            var userMainInfo = new ProfileSimpleUserVM(currentUser);
            return View(userMainInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileSimpleUserVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);

            ViewData["country"] = _countryService.GetAsSelectList();          
            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(vmodel.GenderID);
            if (ModelState.IsValid && user != null)
            {
                user.FirstName = vmodel.FirstName;
                user.LastName = vmodel.LastName;
                user.PatronymicName = vmodel.PatronymicName;
                user.RegionID = vmodel.RegionID;
                user.CityID = vmodel.CityID;
                user.DateOfBirth = GetDate(vmodel.DateOfBirth);
                user.GenderID = vmodel.GenderID;             
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userMainInfo = new ProfileSimpleUserVM(user);
                    return View(userMainInfo);
                }
                else
                {
                    return View(vmodel);
                }
            }

            return View(vmodel);
        }

        public DateTime GetDate(string dateOfBirth)
        {
            return DateTime.Now;
        }
    }
}