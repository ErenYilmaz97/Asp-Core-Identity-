using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Entity.Entities;
using AspNetCoreIdentity.Models;
using AspNetCoreIdentity.PRG;
using AspNetCoreIdentity.SMTP;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AspNetCoreIdentity.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }




        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Member");
            return View();
        }


        [ImportModelState]
        [HttpGet]
        public IActionResult SıgnUp()
        {
            return View();
        }



        
        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> SıgnUp(SignUpModel signUpModel)
        {

            if(!ModelState.IsValid)
            {
                return RedirectToAction("SıgnUp");
            }

            var user = new AppUser()
            {
                UserName = signUpModel.Username,
                Email = signUpModel.Email,
                PhoneNumber = signUpModel.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);


            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

                return RedirectToAction("SıgnUp");
            }

            //KULLANICI OLUŞTURULDU.

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");


            if (!roleResult.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return RedirectToAction("SıgnUp");
            }


            //İŞLEM BAŞARILI!
            return RedirectToAction("Login");

        }



        [ImportModelState]
        [HttpGet]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signinManager.SignOutAsync();
            }

            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }




        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)  //MODEL VALİD Mİ
            {
                return RedirectToAction("Login");
            }

            AppUser user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)  //BÖYLE BİR KULLANICI VAR MI?
            {
                ModelState.AddModelError(string.Empty,"Kullanıcı Bulunamadı.");
                return RedirectToAction("Login");
            }

            if (await _userManager.IsLockedOutAsync(user)) //HESAP KİLİTLİ Mİ?
            {
                ModelState.AddModelError(string.Empty,"Hesabınız Kilitli. Daha Sonra Tekrar Deneyin.");
                return RedirectToAction("Login");
            }

            var result = await _signinManager.PasswordSignInAsync(user,loginModel.Password,loginModel.RememberMe,false);
            


            if (result.Succeeded)  //GİRİŞ YAPILDI. YÖNLENDİRME YAP
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                if (TempData["ReturnUrl"] == null)
                {
                    return RedirectToAction("Index");
                }

                return Redirect(TempData["ReturnUrl"].ToString());
            }

            //GİRİŞ BAŞARISIZSA
            await _userManager.AccessFailedAsync(user); //FAİLED +1
            int failed =await _userManager.GetAccessFailedCountAsync(user);
            if (failed == 3)
            {
                await _userManager.SetLockoutEndDateAsync(user,new System.DateTimeOffset?(DateTimeOffset.Now.AddMinutes(1)));
                ModelState.AddModelError(string.Empty,"Hesabınız 5 Dakikalığına Kilitlenmiştir.");
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, $"{failed} . Hatalı Giriş");
            ModelState.AddModelError(string.Empty,"E-posta veya Şifre Hatalı.");
            return RedirectToAction("Login");
        }




        [ImportModelState]
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }




        [HttpPost]
        [ExportModelState]
        public IActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            AppUser user = _userManager.FindByEmailAsync(forgetPasswordModel.Email).Result;

            if (user == null)
            {
                ModelState.AddModelError(string.Empty,"Kullanıcı Bulunamadı.");
                return RedirectToAction("ForgetPassword");
            }

            string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            string passwordResetLink = Url.Action("ResetPassword", "Home", new
            {
                userID = user.Id,
                token = passwordResetToken,


            }, HttpContext.Request.Scheme);

            PasswordReset.SendPasswordResetMail(passwordResetLink,user.Email);
            ViewBag.status = "Succesfull";


            return RedirectToAction("SentResetPasswordMail");
        }



        [HttpGet]
        public IActionResult SentResetPasswordMail()
        {
            return View();
        }


        [ImportModelState]
        [HttpGet]
        public IActionResult ResetPassword(string userID, string token, string success)
        {
            TempData["userID"] = userID;
            TempData["token"] = token;
            ViewBag.status = success;

            return View();
        }



        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string userID = TempData["userID"].ToString();
            string token = TempData["token"].ToString();

            AppUser user = await _userManager.FindByIdAsync(userID);

            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordModel.NewPassword);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                    return RedirectToAction("ResetPassword");
                }
            }

            //SUCCESS
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("ResetPasswordSuccess");
        }



        [HttpGet]
        public IActionResult ResetPasswordSuccess()
        {
            return View();
        }



        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            if (ReturnUrl.Contains("AnkaraPage"))
            {
                ViewBag.layout = "member";
                ViewBag.message = "Bu Sayfaya Sadece Ankarada Yaşayan Kullanıcılar Erişebilir.";
            }

            else if (ReturnUrl.Contains("ViolencePage"))
            {
                ViewBag.layout = "member";
                ViewBag.message = "Bu Sayfaya 15 Yaşından Büyük Kullanıcılar Erişebilir.";
            }
            else if (ReturnUrl.Contains("ExchangePage"))
            {
                ViewBag.layout = "member";
                ViewBag.message = "30 Günlük Kullanım Süreniz Dolmuştur.";
            }
            else
            {
                ViewBag.layout = "admin";
                ViewBag.message = "Bu Sayfaya Erişiminiz Yok.";
            }


            return View();
        }

       




    }
}
