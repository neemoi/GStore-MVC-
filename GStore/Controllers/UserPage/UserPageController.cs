using GStore.Context;
using GStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GStore.Controllers.UserPage
{
    public class UserPageController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserPageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> UserPageMainAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user);
        }
    }
}
