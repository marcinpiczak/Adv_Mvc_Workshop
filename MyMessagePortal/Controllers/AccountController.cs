using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMessagePortal.Helpers;
using MyMessagePortal.Models;
using MyMessagePortal.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMessagePortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            TempData["Message"] = $"Nie udało się zarejestrować użytkownika {model.Login}";

            if (ModelState.IsValid)
            {
                var user = new UserModel(){UserName = model.Login, Email = model.Email};

                var channel = new ChannelModel()
                {
                    CreatedBy = user,
                    DateAdded = DateTime.Now,
                    IsDefault = true,
                    ChannelColor = ColorHelper.GetRandomColor(),
                    Name = $"Default_{model.Login}"
                };

                user.UserChannels = new List<ChannelModel>
                {
                    channel
                };

                user.ObservedChannels = new List<ObservedChannelsModel>()
                {
                    new ObservedChannelsModel()
                    {
                        Channel = channel,
                        User = user
                    }
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["Message"] = "Rejestracja przebiegła pomyślnie.";

                    var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        TempData["Message"] += $" Jesteś zalogowany jako {model.Login}";

                        return RedirectToAction("Index", "Home");
                    }

                    TempData["Message"] += $" Nie udało się zalogować jako {model.Login}";

                    ModelState.AddModelError("", $"Nie udało się zalogować jako {model.Login}");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = $"Logowanie nie powiodło się dla {model.Login}";

                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    TempData["Message"] = $"Zalogowałeś się jako {model.Login}";

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Wystąpił problem z logowaniem. Sprawdź login i hasło");
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        //utworzono testowo - do usunięcia
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == id);

            await _userManager.DeleteAsync(user);

            TempData["Message"] = $"Usunięto uzytkownika o loginie {user.UserName}";

            return RedirectToAction("index", "Home");
        }
    }
}
