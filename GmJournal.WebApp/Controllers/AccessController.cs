﻿using GmJournal.Data.Entities;
using GmJournal.Logic.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace GmJournal.WebApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserAccessService _userAccessService;

        public AccessController(IUserAccessService userAccessService)
        {
            _userAccessService = userAccessService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                bool loginResoult = await _userAccessService.LoginAsync(user);
                if (loginResoult)
                    return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ViewData["errorMessage"] = $"{ex.Message}";
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _userAccessService.RegisterAsync(user);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ViewData["errorMessage"] = $"{e.Message}";
                }
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            _userAccessService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
