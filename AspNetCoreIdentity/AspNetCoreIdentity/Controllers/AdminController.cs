using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Entity.DbContext;
using AspNetCoreIdentity.Entity.Entities;
using AspNetCoreIdentity.Enums;
using AspNetCoreIdentity.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentity.Controllers
{
    [Controller]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signinManager;


        //DI
        public AdminController(UserManager<AppUser> UserManager, RoleManager<AppRole> RoleManager, SignInManager<AppUser> signinManager)
        {
            _userManager = UserManager;
            _roleManager = RoleManager;
            _signinManager = signinManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Users()
        {
            var result = (from users in _userManager.Users
                select new GetUsersModel
                {
                    Id = users.Id,
                    UserName = users.UserName,
                    Email = users.Email,
                    City = users.City,
                    BirthDay = users.BirthDay,
                   Gender = ((Gender)users.Gender).ToString()
                    
                }).ToList();
            return View(result);
        }



        [HttpGet]
        public IActionResult Roles()
        {
            return View(_roleManager.Roles.ToList());
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleModel);
            }

            AppRole role = new AppRole() {Name = roleModel.Name};

            var result = await _roleManager.CreateAsync(role);
            

            //BAŞARISIZ İSE
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

                return View(roleModel);
            }
            //SUCCEED
            return RedirectToAction("Roles");




        }




        public async Task<IActionResult> Roledelete(string RoleID)
        {
            var role = await _roleManager.FindByIdAsync(RoleID);

            if (role == null || RoleID == null)
            {
                //ROL BULUNAMAZSA, TABLOYU GÜNCELLE
                return RedirectToAction("Roles");
            }

            var result = await _roleManager.DeleteAsync(role);
            return RedirectToAction("Roles");
        }




        [HttpGet]
        public async Task<IActionResult> RoleUpdate(string RoleID)
        {
            AppRole role = await _roleManager.FindByIdAsync(RoleID);

            if (role==null)
            {
                //SAYFAYI GÜNCELLE
                return RedirectToAction("Roles");
            }

            //SUCCEED 
            return View(role.Adapt<RoleModel>());
        }



        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleModel);
            }
            AppRole role = await _roleManager.FindByIdAsync(roleModel.Id);

            if (role == null)
            {
                //ROL BULUNAMAZSA TABLOYU GÜNCELLE
                return RedirectToAction("Roles");
            }

            role.Name = roleModel.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                    
                }
                return RedirectToAction("RoleUpdate");
            }
            //SUCCEED
            return RedirectToAction("Roles");


        }



        [HttpGet]
        public async Task<IActionResult> RoleAssign(string UserID)
        {
            var user = await _userManager.FindByIdAsync(UserID);

            if (user == null)
            {
                return RedirectToAction("Users");
            }
            //TÜM ROLLER
            List<AppRole> AllRoles = _roleManager.Roles.ToList();

            //KULLANICININ SAHİP OLDUĞU ROLLER
            var UserRoles = await _userManager.GetRolesAsync(user) as List<string>;

            RoleAssignModel roleAssignModel = new RoleAssignModel{UserID = user.Id, Username = user.UserName};


            foreach (var role in AllRoles)
            {
               roleAssignModel.Roles.Add(new Role
               {
                   RoleID = role.Id,
                   RoleName = role.Name,
                   Exist = UserRoles.Contains(role.Name)? true : false
               });
            }

            return View(roleAssignModel);

        }




        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignModel roleAssignModel)
        {
            var user = await _userManager.FindByIdAsync(roleAssignModel.UserID);

            if (user == null)
            {
                return RedirectToAction("Users");
            }

            foreach (Role role in roleAssignModel.Roles)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }

            await _signinManager.SignOutAsync();
            await _signinManager.SignInAsync(user, true);
            return RedirectToAction("Users");
        }



        [HttpGet]
        public IActionResult Claims()
        {
            return View(User.Claims.ToList());
        }



       
    }
}
