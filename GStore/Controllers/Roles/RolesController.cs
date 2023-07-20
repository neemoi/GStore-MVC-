using GStore.Models;
using GStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace GStore.Controllers.Roles
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
		readonly RoleManager<IdentityRole> _roleManager;
		readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public IActionResult RoleList() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            User? user = await _userManager.FindByNameAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Roles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
               await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("RoleList", "Roles");
        }

        public async Task<IActionResult> Edit(string userId)
        {
            User? user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var allRoles = _roleManager.Roles.ToList();

                ChangeRoleViewModel model = new()
				{
                    UserId = user.Id,
                    UserEmail = user?.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                return View(model);
            }

            return RedirectToAction("UserList", "AdminPanelUsers");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User? user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var addedRoles = roles.Except(userRoles);

                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList", "AdminPanelUsers");
            }

            return RedirectToAction("UserList", "AdminPanelUsers");
        }
    }
}
