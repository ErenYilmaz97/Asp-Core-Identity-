using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreIdentity.Entity.Entities;
using AspNetCoreIdentity.Models;
using AspNetCoreIdentity.PRG;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentity.Controllers
{
   
    [Controller]
    [Authorize]
    public class MemberController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }




        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            UserInfoModel userInfoModel = user.Adapt<UserInfoModel>();
            return View(userInfoModel);
        }



        [ImportModelState]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }



        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword");
            }

            //ZATEN KULLANICI NULL OLAMAZ.
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkPassword = await _userManager.CheckPasswordAsync(user, changePasswordModel.OldPassword);

            //GİRİLEN ŞİFRE DOĞRU MU?
            if (!checkPassword)
            {
                ModelState.AddModelError(string.Empty,"Eski Şifreniz Yanlış.");
                return RedirectToAction("ChangePassword");
            }


            var result = await _userManager.ChangePasswordAsync(user,changePasswordModel.OldPassword,changePasswordModel.NewPassword);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

                return RedirectToAction("ChangePassword");
            }

            //İŞLEM SUCCEED
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("Index");



        }



        [ImportModelState]
        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            //KULLANICI BİLGİLERİNİ MODELE SETLEYİP VİEW'A YOLLA.
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditModel userEditModel = user.Adapt<UserEditModel>();

            return View(userEditModel);
        }




        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> UserEdit(UserEditModel userEditModel,IFormFile UserPicture)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UserEdit");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            //BİR RESİM YÜKLENDİYSE
            if (UserPicture != null && UserPicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(UserPicture.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await UserPicture.CopyToAsync(stream);

                    user.Picture = "/Pictures/" + fileName;
                }
            }


            
            
            user.UserName = userEditModel.UserName;
            user.PhoneNumber = userEditModel.PhoneNumber;
            user.City = userEditModel.City;
            user.BirthDay = userEditModel.BirthDay;
            user.Gender = (int)userEditModel.Gender;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

                return RedirectToAction("UserEdit");
            }
                
            //SUCCEED
            await _userManager.UpdateSecurityStampAsync(user);
            await _signinManager.SignOutAsync();
            await _signinManager.SignInAsync(user, true);
            return RedirectToAction("Index");
            
        }





        [HttpGet]
        public async void Logout()
        {
            await _signinManager.SignOutAsync();
        }




        [HttpGet]
        [Authorize(Roles = "Edıtor")]
        public IActionResult EditorPage()
        {
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult ManagerPage()
        {
            return View();
        }



        [HttpGet]
        [Authorize(Policy = "AnkaraPolicy")]
        public IActionResult AnkaraPage()
        {
            return View();
        }



        [HttpGet]
        [Authorize(Policy = "ViolencePolicy")]
        public IActionResult ViolencePage()
        {
            return View();
        }



        public async Task<IActionResult> ExchangeRedirect()
        {
            //CURRENT USER
            var user = await _userManager.GetUserAsync(User);
            
            if (!User.HasClaim(x => x.Type == "DateExchange"))
            {
                //BÖYLE BİR CLAİM YOKSA, BU SAYFAYA İLK KEZ ERİŞİYOR. CLAIM OLUŞTUR
                Claim DateExchange = new Claim("DateExchange",DateTime.Now.AddDays(30).Date.ToShortDateString(),ClaimValueTypes.String,"Internal");

                //VERİTABANINA CLAİMİ EKLE
                await _userManager.AddClaimAsync(user, DateExchange);
                await _signinManager.SignOutAsync();
                await _signinManager.SignInAsync(user, true);

            }

            return RedirectToAction("ExchangePage");
        }


        [HttpGet]
        [Authorize(Policy = "ExchangePolicy")]
        public IActionResult ExchangePage()
        {
            return View();
        }

    }
}
