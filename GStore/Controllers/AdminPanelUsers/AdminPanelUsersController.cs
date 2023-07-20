using GStore.Context;
using GStore.Models;
using GStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Intrinsics.X86;

namespace GStore.Controllers.UserManagment
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelUsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GoogleStoreContext _googleStoreContext;

        public AdminPanelUsersController(UserManager<User> userManager, GoogleStoreContext googleStoreContext)
        {
            _userManager = userManager;
            _googleStoreContext = googleStoreContext;
        }

        [HttpGet]
        public async Task<IActionResult> UserList() => View(await _userManager.Users.ToListAsync());

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmailUser(string email)
        {
            var usersByEmail = await _userManager.Users.Where(u => u.Email == email).ToListAsync();

            if (usersByEmail.Count == 0)
            {
                TempData["ErrorMessage"] = $"Пользователь не найден.";

                return RedirectToAction("UserList", "AdminPanelUsers");
            }
            else
            {
                return View(usersByEmail);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var model = new EditUserViewModel()
            {
                Email = user.Email,
                Password= user.Password,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Country = user.Country,
                City = user.City,
                Address = user.Address
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.Email = model.Email ?? user.Email;
            user.Password = model.Password ?? user.Password;
            user.Name = model.Name ?? user.Name;
            user.Phone = model.Phone ?? user.Phone;
            user.Country = model.Country ?? user.Country;
            user.City = model.City ?? user.City;
            user.Address = model.Address ?? user.Address;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("EditUser", "AdminPanelUsers");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password,
                    UserName = model.Email,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    Phone = model.Phone,
                };
                
                await _userManager.AddToRoleAsync(user, "User");

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserList", "AdminPanelUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);

            if (id != "234545f7-fa35-4703-8ce4-15da020f7576")
            {
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        await _googleStoreContext.SaveChangesAsync();
                        
                        return RedirectToAction("UserList", "AdminPanelUsers");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Вы пытаетесь удалить роль Admin");

                return RedirectToAction("UserList", "AdminPanelUsers");
            }

            return RedirectToAction("Index");
        }
    }
}
